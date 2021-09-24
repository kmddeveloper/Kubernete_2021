using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface IRegistrationService
    {
        Task<User> RegisterUserAsync(User user);
    }
}
