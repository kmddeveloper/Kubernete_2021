using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface ICartService
    {

        Task<ShoppingCart> AddItemToCartAsync(CartFields cartFields);
        Task<ShoppingCart> GetCartItemsAsync(string userId);
        Task<ShoppingCart> UpdateCartAsync(string userId, int cartId, int quantity);
        Task<ShoppingCart> DeleteCartAsync(string userId, int cartId);
        //Task<List<ItemInCart>> GetLocalCartItems(List<CartFields> cartItems);

        //Task<List<ItemInCart>> GetItemsInCartAsync(string idList, Dictionary<int, int> quantityLookUpCol = null);
    }
}
