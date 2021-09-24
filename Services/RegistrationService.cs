using EFModel;
using Kubernetes.Repository;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public class RegistrationService: IRegistrationService
    {
        IRegistrationRepository _registrationRepository;
        IUserRepository _userRepository;
        ILogger _logger;

        public RegistrationService(IRegistrationRepository registrationRepository, IUserRepository userRepository, ILogger<RegistrationService> logger)
        {
            _registrationRepository = registrationRepository;
            _userRepository = userRepository;
            _logger = logger;
        }


        #region RegisterUser
        public async Task<User> RegisterUserAsync(User user)
        {
            return await _registrationRepository.CreateUserAsync(user);
        } 
        #endregion
    }
}
