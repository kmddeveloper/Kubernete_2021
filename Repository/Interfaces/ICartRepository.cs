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
        Task<List<ItemInCart>> UpdateCartAsync(string userId, int cartId, int quantity);
        Task<List<ItemInCart>> DeleteCartAsync(string userId, int cartId);
    }
}
