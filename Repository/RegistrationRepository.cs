using EFModel;
using Kubernetes.DataReaderMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        readonly IDataReaderMapper _dataReaderMapper;
        readonly ILogger _logger;
        
        public RegistrationRepository(IDataReaderMapper dataReaderMapper, ILogger<RegistrationRepository> logger)
        {
            _dataReaderMapper = dataReaderMapper;
            _logger = logger;
        }

        #region CreateUserAsync
        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                var map = _dataReaderMapper.CreateMap()
                   .ForMember<User, Int32, SourceDataField>(d => d.Id, s => s.GetFieldName("id"))
                   .ForMember<User, String, SourceDataField>(d => d.UserName, s => s.GetFieldName("username"))
                   .ForMember<User, String, SourceDataField>(d => d.Password, s => s.GetFieldName("password"))
                   .ForMember<User, String, SourceDataField>(d => d.First_Name, s => s.GetFieldName("first_name"))
                   .ForMember<User, String, SourceDataField>(d => d.Last_Name, s => s.GetFieldName("last_name"))
                   .ForMember<User, String, SourceDataField>(d => d.Email, s => s.GetFieldName("email"));

                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_createUser"
                };
                sqlCmd.Parameters.AddWithValue("@email", user.Email);
                sqlCmd.Parameters.AddWithValue("@password", user.Password);
                sqlCmd.Parameters.AddWithValue("@first_name", user.First_Name);
                sqlCmd.Parameters.AddWithValue("@last_name", user.Last_Name);
                sqlCmd.Parameters.AddWithValue("@user_category_id", 2);
                var data = await _dataReaderMapper.MapAsync<User>(sqlCmd, map);
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("RegistrationRepository:CreateUserAsync:{0}", e.Message);
            }
            return null;
        } 
        #endregion
    }
}
