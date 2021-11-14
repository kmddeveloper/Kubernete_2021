using EFModel;
using Kubernetes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{


    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        readonly ICartService _cartService;

        public CartController(ICartService cartService, ILogger<CartController> logger) : base(logger)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        //Get Cart
        public async Task<ApiResponse<ShoppingCart>> GetAsync(string userId)
        {
            var shoppingCart = await _cartService.GetCartItemsAsync(userId);
            return ApiResult<ShoppingCart>(shoppingCart);
        }

        [HttpPost] 
        //Add item to cart
        public async Task<ApiResponse<ShoppingCart>> PostAsync([FromBody]CartFields cartFields)
        {            

            var shoppingCart = await _cartService.AddItemToCartAsync(cartFields);
            return ApiResult<ShoppingCart>(shoppingCart);
        }


        [HttpPut]
        //Update cart quantities
        public async Task<ApiResponse<ShoppingCart>> PutAsync([FromBody] CartUpdateFields cartUpdateFields)
        {
            var shoppingCart = await _cartService.UpdateCartAsync(cartUpdateFields.UserId, cartUpdateFields.CartId, cartUpdateFields.Quantity);
            return ApiResult<ShoppingCart>(shoppingCart);
        }


        [HttpDelete]
        //Update cart quantities
        public async Task<ApiResponse<ShoppingCart>> DeleteAsync([FromBody] CartUpdateFields cartUpdateFields)
        {
            var shoppingCart = await _cartService.DeleteCartAsync(cartUpdateFields.UserId, cartUpdateFields.CartId);
            return ApiResult<ShoppingCart>(shoppingCart);
        }
    }
}
