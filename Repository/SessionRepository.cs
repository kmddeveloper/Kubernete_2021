using EFModel;
using Kubernetes.DataReaderMapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public class SessionRepository : ISessionRepository
    {
        readonly IDataReaderMapper _dataReaderMapper;
        readonly IMaps _maps;



        public SessionRepository(IDataReaderMapper dataReaderMapper, IMaps maps)
        {
            _dataReaderMapper = dataReaderMapper;
            _maps = maps;
        }


        #region CreateUserSessionAsync
        public async Task<UserSession> CreateAsync(int userId)
        {
            try
            {
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_create_user_session"
                };
                sqlCmd.Parameters.AddWithValue("@user_id", userId);

                var data = await _dataReaderMapper.MapAsync<UserSession>(sqlCmd, _maps.SessionMap);

                if (data == null || data.UserId <= 0)
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


        #region GetUserSessionAsync
        public async Task<UserSession> GetAsync(string sessionId)
        {
            try
            {
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getUserSession"
                };
                sqlCmd.Parameters.AddWithValue("@sessionId", sessionId);

                var data = await _dataReaderMapper.MapAsync<UserSession>(sqlCmd, _maps.SessionMap);

                if (data == null || data.UserId <= 0)
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
    }
}
