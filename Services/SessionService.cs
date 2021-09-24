using EFModel;
using Kubernetes.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public class SessionService : ISessionService
    {
        readonly ISessionRepository _sessionRepository;
        readonly ILogger _logger;

        public SessionService(ISessionRepository sessionRepository, ILogger<SessionService> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
        }

        #region CreateUserSessionAsync
        public async Task<UserSession> CreateAsync(int userId)
        {
            return await _sessionRepository.CreateAsync(userId);
        } 
        #endregion

        #region GetUserSessionAsync
        public async Task<UserSession> GetAsync(string sessionId)
        {
            if (String.IsNullOrEmpty(sessionId))
                throw new Exception("SessionId cannot be null or empty!");

            return await _sessionRepository.GetAsync(sessionId);
        } 
        #endregion
    }
}
