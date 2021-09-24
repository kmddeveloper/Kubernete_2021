using EFModel;
using Kubernetes.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int Id);
        Task<User> GetUserByEmailAsync(string email);
        User GetUserById(int Id); 
    }
}
