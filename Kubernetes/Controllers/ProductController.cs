using EFModel;
using Kubernetes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Filters;
using System.Linq;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    
    [Route("api/[controller]")]
   
    public class ProductController : BaseController
    {
        readonly IProductService _productService;        
        public ProductController(IProductService productService,  ILogger<ProductController> logger):base(logger)
        {
            _productService = productService;            

        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("[action]/{pageNum}/{pageSize}/{categoryId}")]
        [HttpGet]
        public async Task<ApiResponse<List<Product>>> Browse(int pageNum, int pageSize, int categoryId)
        {
            return ApiResult(await _productService.GetProductsAsync(pageNum, pageSize, categoryId));               
        }

      

        [Route("[action]/{categoryId}")]
        [HttpGet]
        public async Task<ApiResponse<int>> TotalItems(int categoryId)
        {
            return ApiResult(await _productService.GetTotalItemsByCategoryId(categoryId));
        }


        [Route("[action]/{code}")]
        [HttpGet]
        public async Task<ApiResponse<ProductEditItem>> EditItem (string code)
        {
           return ApiResult(await _productService.GetEditItemAsync(code));
            
        }

        //public async Task<ApiResponse<ProductEditItem>> EditItem(string code)
        //{
        //    var item = await _productService.GetEditItemAsync(code);

        //    return ApiResult<ProductEditItem>(item);
        //}

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

  
        // PUT api/<ProductController>/5
        
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            await  _productService.UpdateProductItem(product);
            return Ok("success");
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
