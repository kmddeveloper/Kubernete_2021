using EFModel;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kubernetes.Services
{

    public class AuthService:IAuthService
    {
        readonly IUserService _userService;
        readonly ISessionService _sessionService;
        readonly ILogger _logger;
        public AuthService(IUserService userService, ISessionService  sessionService,  ILogger<AuthService> logger)
        {
            _userService = userService;
            _sessionService = sessionService;
            _logger = logger;
        }

        #region LoginAsync
        public async Task<UserSession> LoginAsync(string email, string password)
        {
            var user = await VerifyUserAsync(email, password);

            if (user != null && user.Id > 0)
            {
                var userSession = await _sessionService.CreateAsync(user.Id);

                if (userSession == null || String.IsNullOrEmpty(userSession.Id))
                    throw new Exception($"Unable to create userSession: InnerEception");

                return userSession;

            }
            return null;
        } 
        #endregion

        #region VerifyUserAsync
        public async Task<User> VerifyUserAsync(string email, string password)
        {
            if (String.IsNullOrEmpty(email))
                throw new Exception("Email address cannot be empty!");
            if (String.IsNullOrEmpty(password))
                throw new Exception("Email password cannot be empty!");

            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null || user.Id == 0)
                throw new Exception("Invalid email or password!");


            if (String.Compare(user.Password, password) != 0)
                throw new Exception("Invalid password!");

            return user;
        } 
        #endregion
    }
}
