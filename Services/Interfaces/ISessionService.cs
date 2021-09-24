using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface ISessionService
    {
        Task<UserSession> CreateAsync(int userId);
        Task<UserSession> GetAsync(string sessionId);
    }
}
