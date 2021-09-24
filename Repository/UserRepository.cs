using EFModel;
using Kubernetes.DataReaderMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly IDataReaderMapper _dataReaderMapper;
        readonly IMaps _maps;

        public UserRepository(IDataReaderMapper dataReaderMapper, IMaps maps)
        {
            _dataReaderMapper = dataReaderMapper;
            _maps = maps;
        }

        public User GetUserById(int Id)
        {
            return null;
        }
        


        #region GetUserByCategoryAsync
        public async Task<List<User>> GetUserByCategoryAsync(string userCategory)
        {
            try
            {           
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getUserByCategory"
                };
                sqlCmd.Parameters.AddWithValue("@userCategory", userCategory);

                var data = await _dataReaderMapper.MapToListAsync<User>(sqlCmd, _maps.UserMap);

                if (data == null || data[0].Id <=0)
                    return null;
                return data;
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }
        #endregion


        #region GetUserByIdAsync
        public async Task<User> GetUserByIdAsync(int Id)
        {                          
            var sqlCmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "appV1_getUserById"
            };
            sqlCmd.Parameters.AddWithValue("@id", Id);             

            var data = await _dataReaderMapper.MapAsync<User>(sqlCmd, _maps.UserMap);

            if (data == null)
                throw new Exception($"Unable to get user with id={Id}");
            return data;               
        }
        #endregion


        #region GetUserByEmailAsync
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {             
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getUserByEmail"
                };
                sqlCmd.Parameters.AddWithValue("@email", email);

                var data = await _dataReaderMapper.MapAsync<User>(sqlCmd, _maps.UserMap);

                if (data == null || String.IsNullOrEmpty(data.Email))
                    return null;
                return data;
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }
        #endregion

        public async Task Create(User user)
        {
            await Task.Delay(10);
            
        }

    }
}
