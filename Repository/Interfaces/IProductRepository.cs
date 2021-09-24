using EFModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(int pageNum, int pageSize, int category);

        Task<Product> GetProductByCodeAsync(string code);

        Task<int> GetTotalItemsByCategoryIdAsync(int category);

        Task<List<DropdownField<int, string>>> GetProductCategoriesAsync();
        Task<List<DropdownField<int, string>>> GetProductStatusAsync();

        Task<bool> CheckProductCodeExists(int id, string code);
        Task UpdateProductAsync(Product product);

        Task<List<Product>> GetProductByIdsAsync(string Ids);

        Task<List<ProductSpecRaw>> GetProductSpecAsync(int productId);


    }
}
