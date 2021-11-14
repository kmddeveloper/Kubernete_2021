using EFModel;
using Kubernetes.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public class ProductService : IProductService
    {
        readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProductsAsync(int page = 1, int pageSize = 10, int categoryId = 1)
        {
            return await _productRepository.GetProductsAsync(page, pageSize, categoryId);
        }


        public async Task<int> GetTotalItemsByCategoryId(int categoryId=1)
        {
            return await _productRepository.GetTotalItemsByCategoryIdAsync(categoryId);
        }

        public async Task<Product> GetProductByCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new System.Exception("Invalid product code!");

            return  await _productRepository.GetProductByCodeAsync(code);
        }


        public async Task<ProductEditItem> GetEditItemAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new System.Exception("Invalid product code!");            

            var productTask = _productRepository.GetProductByCodeAsync(code);
            var categoryTask= _productRepository.GetProductCategoriesAsync();
            var statusTask= _productRepository.GetProductStatusAsync();

            await Task.WhenAll(productTask, categoryTask, statusTask);

            var product = productTask.Result;

            categoryTask.Result.Find(x => x.Key == product.CategoryId);
            statusTask.Result.Find(x => x.Key == product.StatusId);

            return new ProductEditItem
            {
                CategoryId = product.CategoryId,
                StatusId = product.StatusId,
                Code = product.Code,
                Description = product.Description,
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Note = product.Note,
                Price = product.Price,
                CategoryList = categoryTask.Result,            
                StatusList = statusTask.Result
            };
        }


        public async Task UpdateProductItem(Product product)
        {
            if (product == null)
                throw new Exception("Invalid product object!");

            await _productRepository.UpdateProductAsync(product);        
        }



        public async Task<List<ProductSpec>> GetProductSpecsAsync(int productId)
        {
            if (productId <=0)
                throw new Exception("Invalid product Id!");

            var productSpecRawContents =  await _productRepository.GetProductSpecAsync(productId);
            
            if (productSpecRawContents != null && productSpecRawContents.Count > 0)
            {
                var productSpecList = new List<ProductSpec>(productSpecRawContents.Count);
                int specId = 0;

                ProductSpec prodSpec = null;
                foreach (var productSpecRawContent in productSpecRawContents)
                {
                    if (productSpecRawContent != null)
                    {
                        if (specId!=productSpecRawContent.SpecId)
                        {
                            if (prodSpec != null)
                                productSpecList.Add(prodSpec);
                            prodSpec = new ProductSpec{Title=productSpecRawContent.Title, Fields = new List<Field<string, string>>()};
                        }
                        if (prodSpec != null && prodSpec.Fields!=null)
                        {
                            prodSpec.Fields.Add(new Field<string, string> { Key = productSpecRawContent.ContentName, Value = productSpecRawContent.ContentValue });
                        }
                    }
                }
                return productSpecList;
            }
            return null;

        }


        public async Task<ProductDetail> GetProductDetailAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new System.Exception("Invalid product code!");

            var product =  await _productRepository.GetProductByCodeAsync(code);

            if (product != null && product.Id > 0)
            {
                var specs = GetProductSpecsAsync(product.Id);
                var images = _productRepository.GetProductImagesAsync(product.Id, 0);
                var features = _productRepository.GetProductFeaturesAsync(product.Id);
                var attrubutes = _productRepository.GetProductAttributesAsync(product.Id);
                await Task.WhenAll(specs, images, features, attrubutes);

                return new ProductDetail
                {
                    Features = features.Result,
                    Item = product,
                    ItemImages = images.Result,
                    Specs = specs.Result,
                    Attributes = attrubutes.Result
                };
            }
            return null; 

        }


        public async Task<int> GetProductItemIdByColorSize(int productId, int sizeId, int colorId)
        {
            if (productId > 0 && sizeId > 0 && colorId > 0)
                return await _productRepository.GetProductItemIdByColorSizeAsync(productId, sizeId, colorId);
            return 0;
        }       

        public async Task<List<ProductColor>> GetProductColorsAsync(int productId, int sizeId=0)
        {
            if (productId > 0)
                return await _productRepository.GetProductColorsAsync(productId, sizeId);
            return null;
        }

        public async Task<List<ProductSize>> GetProductSizesAsync(int productId, int colorId = 0)
        {
            if (productId > 0)
                return await _productRepository.GetProductSizesAsync(productId, colorId);
            return null;
        }

        //public IQueryable<Product> GetProducts(int page=1, int pageSize=10, int categoryId=1)
        //{
        //    return _context.Products.FromSqlInterpolated($"EXEC appV1_getProductPaging {page}, {pageSize}, {categoryId}");                            
        //}
    }
}
