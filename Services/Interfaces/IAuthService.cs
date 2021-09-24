using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface IAuthService
    {
        Task<UserSession> LoginAsync(string email, string password);
        Task<User> VerifyUserAsync(string email, string password);     
    }
}
