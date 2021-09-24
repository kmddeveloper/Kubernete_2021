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

        Task<List<ProductSpec>> GetProductSpecs(int productId);
    }
}
