using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public interface ICartRepository
    {
        Task<List<ItemInCart>> AddItemToCartAsync(CartFields cartFields);
        Task<List<ItemInCart>> GetCartItemsAsync(string userId);
    }
}
