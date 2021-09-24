using EFModel;
using Kubernetes.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int Id);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetUserByCategoryAsync(string userCategory);
        User GetUserById(int Id);
    }
}
