using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public interface ISessionRepository
    {
        Task<UserSession> CreateAsync(int userId);
        Task<UserSession> GetAsync(string sessionId);
    }
}
