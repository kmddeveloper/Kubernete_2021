using EFModel;
using Kubernetes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]

    public class ProductDetailController : BaseController
    {
        readonly IProductService _productService;
        public ProductDetailController(IProductService productService, ILogger<ProductDetailController> logger) : base(logger)
        {
            _productService = productService;
        }

        [HttpGet("{code}")]
        public async Task<ApiResponse<ProductDetail>> Get(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException("Invalid product code!");
            return ApiResult<ProductDetail>(await _productService.GetProductDetailAsync(code));
        }

        [Route("RequestItemId")]
        [HttpPost]
        public async Task<ApiResponse<int>> GetProductItemIdAsync([FromBody] RequestProductItemId request)
        {
            var productIdId = await _productService.GetProductItemIdByColorSize(request.ProductId, request.SizeId, request.ColorId);
            return ApiResult<int>(productIdId);
        }

        [Route("AvailableColors/{productId}/{sizeId}")]
        [HttpGet]
        public async Task<ApiResponse<List<ProductColor>>> GetProductColorsAsync(int productId, int sizeId=0)
        {
            var availableColors = await _productService.GetProductColorsAsync(productId, sizeId);
            return ApiResult<List<ProductColor>>(availableColors);
        }


        [Route("AvailableSizes/{productId}/{colorId}")]
        [HttpGet]
        public async Task<ApiResponse<List<ProductSize>>> GetProductSizesAsync(int productId, int colorId = 0)
        {
            var availableSizes = await _productService.GetProductSizesAsync(productId, colorId);
            return ApiResult<List<ProductSize>>(availableSizes);
        }

    }
}
