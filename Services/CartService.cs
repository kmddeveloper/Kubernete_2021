using EFModel;
using Kubernetes.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Kubernetes.Services
{

  
    public class CartService: ICartService
    {
        readonly ICartRepository _cartRepository;
        readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<ShoppingCart> AddItemToCartAsync(CartFields cartFields)
        {
            var itemsInCart =  await _cartRepository.AddItemToCartAsync(cartFields);

            return new ShoppingCart
            {
                ItemsInCart = itemsInCart,
                Count = itemsInCart==null? 0: itemsInCart.Sum(x=> x.Quantity)
            };
        }

        public async Task<ShoppingCart> GetCartItemsAsync(string userId)
        {
            var itemsInCart = await _cartRepository.GetCartItemsAsync(userId);

            return new ShoppingCart
            {
                ItemsInCart = itemsInCart,
                Count = itemsInCart == null ? 0 : itemsInCart.Sum(x => x.Quantity)
            };
        }


        /*
        public async Task<List<ItemInCart>> GetLocalCartItems(List<CartFields> cartItems)
        {
            var quantityLookUpCol = new Dictionary<int, int>();
            var idList = new StringBuilder(128);

            if (cartItems != null && cartItems.Count > 0)
            {
                foreach (var item in cartItems)
                {
                    if (item != null)
                    {
                        if (!quantityLookUpCol.ContainsKey(item.ProductId))
                        {
                            quantityLookUpCol.Add(item.ProductId, item.Quantity);
                            if (idList.Length > 0) idList.Append(",");
                            idList.Append(idList.Length > 0 ? $",{item.ProductId.ToString()}" : item.ProductId.ToString());
                        }
                    }
                }
                return await GetItemsInCartAsync(idList.ToString(), quantityLookUpCol);
            }
            return null;
        }

        public async Task<List<ItemInCart>>GetItemsInCartAsync(string idList, Dictionary<int,int> quantityLookUpCol = null)
        {
            if (idList.Length > 0)
            {
                var products = await _productRepository.GetProductByIdsAsync(idList);

                if (products != null && products.Count > 0)
                {
                    var itemsInCart = new List<ItemInCart>();
                    foreach (var product in products)
                    {
                        if (product != null)
                        {
                            itemsInCart.Add(new ItemInCart
                            {
                                Code = product.Code,
                                Description = product.Description,
                                Id = product.Id,
                                ImageUrl = product.ImageUrl,
                                Name = product.Name,
                                Price = product.Price, 
                                Quantity = quantityLookUpCol != null && quantityLookUpCol.ContainsKey(product.Id) ? quantityLookUpCol[product.Id] : 1
                            });
                        }
                    }
                    return itemsInCart;
                }
            }
            return null;
        }
        */
    }
}
