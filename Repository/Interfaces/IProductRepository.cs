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

        Task<List<ProductFeature>> GetProductFeaturesAsync(int productId);

        Task<List<ProductImage>> GetProductImagesAsync(int productId, int productItemId);

        Task<List<ProductAttribute>> GetProductAttributesAsync(int productId);
        Task<int> GetProductItemIdByColorSizeAsync(int productId, int sizeId, int colorId);

        Task<List<ProductSize>> GetProductSizesAsync(int productId, int colorId = 0);
        Task<List<ProductColor>> GetProductColorsAsync(int productId, int sizeId = 0);


    }
}
