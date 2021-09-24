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
        public async Task<ApiResponse<Product>> Get(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException("Invalid product code!");
            
            return ApiResult<Product>(await _productService.GetProductByCodeAsync(code));
        }

    }
}
