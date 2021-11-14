using EFData;
using EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(int page, int pageSize, int categoryId = 1);
        Task<int> GetTotalItemsByCategoryId(int categoryId=1);
        Task<ProductEditItem> GetEditItemAsync(string code);
        Task UpdateProductItem(Product product);
        Task<Product> GetProductByCodeAsync(string code);

        Task<List<ProductSpec>> GetProductSpecsAsync(int productId);
        Task<ProductDetail> GetProductDetailAsync(string code);
        Task<int> GetProductItemIdByColorSize(int productId, int sizeId, int colorId);

        Task<List<ProductSize>> GetProductSizesAsync(int productId, int colorId = 0);
        Task<List<ProductColor>> GetProductColorsAsync(int productId, int sizeId = 0);
    }
}
