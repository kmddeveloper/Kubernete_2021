using EFModel;
using Extensions;
using Kubernetes.DataReaderMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{

    public class ProductRepository: IProductRepository
    {
        readonly IDataReaderMapper _dataReaderMapper;
        readonly IMaps _maps;

        public ProductRepository(IDataReaderMapper dataReaderMapper, IMaps maps)
        {
            _dataReaderMapper = dataReaderMapper;
            _maps = maps;
        }


        public async Task<List<Product>> GetProductsAsync(int pageNum, int pageSize, int category)
        {
            try
            {
                var map = _maps.ProductMap;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getProductPaging"
                };
                sqlCmd.Parameters.AddWithValue("@pageNum", pageNum);
                sqlCmd.Parameters.AddWithValue("@pageSize", pageSize);
                sqlCmd.Parameters.AddWithValue("@category_id", category);

                return await _dataReaderMapper.MapToListAsync<Product>(sqlCmd, map);
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }
        public async Task<Product> GetProductByCodeAsync(string code)
        {
            try
            {
                var map = _maps.ProductMap;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getProductItemByCode"
                };
                sqlCmd.Parameters.AddWithValue("@code", code);
               

                return await _dataReaderMapper.MapAsync<Product>(sqlCmd, map);
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }


        
        //appV1_getTotalItemsByCategoryId

        public async Task<int> GetTotalItemsByCategoryIdAsync(int category)
        {
            try
            {
                var map = _maps.ProductMap;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getTotalItemsByCategoryId"
                };
                sqlCmd.Parameters.AddWithValue("@category_id", category);
               
                var o =  await _dataReaderMapper.ExecuteScalarAsync(sqlCmd);
                return o.ToInt32();

            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return 0;
        }



        public async Task<List<Product>> GetProductByIdsAsync(string Ids)
        {
            try
            {
                var map = _maps.ProductMap;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getProductByIds"
                };
                sqlCmd.Parameters.AddWithValue("@productIds", Ids);

                return  await _dataReaderMapper.MapToListAsync<Product>(sqlCmd, map); 
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }


        public async Task<List<DropdownField<int,string>>> GetProductCategoriesAsync()
        {
            try
            {
                var map = _maps.ProductCategory;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getProductCategories"
                };                
                return await _dataReaderMapper.MapToListAsync<DropdownField<int,string>>(sqlCmd, map);               
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }

        public async Task<List<DropdownField<int, string>>> GetProductStatusAsync()
        {
            try
            {
                var map = _maps.ProductStatus;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getProductStatus"
                };
                
                return await _dataReaderMapper.MapToListAsync<DropdownField<int, string>>(sqlCmd, map);
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }

        public async Task<bool> CheckProductCodeExists(int id, string code)
        {
            try
            {                
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "appV1_getProductStatus";
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    sqlCmd.Parameters.AddWithValue("@code", code);

                    var o = await _dataReaderMapper.ExecuteScalarAsync(sqlCmd);

                    if (o.ToInt32() > 0)
                        return true;
                }
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return false;
        }


        public async Task UpdateProductAsync(Product product)
        {                       
            var sqlCmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "appV1_updateProductItem"
            };
            sqlCmd.Parameters.AddWithValue("@id", product.Id);
            sqlCmd.Parameters.AddWithValue("@name", product.Name);
            sqlCmd.Parameters.AddWithValue("@description", product.Description);

            sqlCmd.Parameters.AddWithValue("@code", product.Code);
            sqlCmd.Parameters.AddWithValue("@price", product.Price);
            sqlCmd.Parameters.AddWithValue("@image_url", product.ImageUrl);
            sqlCmd.Parameters.AddWithValue("@status_id", product.StatusId);
            sqlCmd.Parameters.AddWithValue("@note", product.Note);
            sqlCmd.Parameters.AddWithValue("@category_id", product.CategoryId);

            await _dataReaderMapper.ExecuteAsync(sqlCmd);
               
        }

        public async Task<List<ProductSpecRaw>> GetProductSpecAsync(int productId)
        {
            try
            {
                var map = _maps.ProductSpec;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getProductStatus"
                };

                return await _dataReaderMapper.MapToListAsync<ProductSpecRaw>(sqlCmd, map);
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }
    }
}
