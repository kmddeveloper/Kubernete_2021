using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public interface IRegistrationRepository
    {

        Task<User> CreateUserAsync(User user);


    }
}
