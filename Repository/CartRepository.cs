using EFModel;
using Kubernetes.DataReaderMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Repository
{
    public class CartRepository: ICartRepository
    {
        readonly IDataReaderMapper _dataReaderMapper;
        readonly IMaps _maps;

        public CartRepository(IDataReaderMapper dataReaderMapper, IMaps maps)
        {
            _dataReaderMapper = dataReaderMapper;
            _maps = maps;
        }

        public async Task<List<ItemInCart>> AddItemToCartAsync(CartFields cartFields)
        {
            try
            {
                var map = _maps.ItemInCartMap;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_addItemToCart"
                };
                sqlCmd.Parameters.AddWithValue("@product_id", cartFields.ProductId);
                sqlCmd.Parameters.AddWithValue("@quantity", cartFields.Quantity);
                sqlCmd.Parameters.AddWithValue("@user_id", cartFields.UserId);

                return await _dataReaderMapper.MapToListAsync<ItemInCart>(sqlCmd, map);
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }

        public async Task<List<ItemInCart>> GetCartItemsAsync(string userId)
        {
            try
            {
                var map = _maps.ItemInCartMap;
                var sqlCmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "appV1_getCartItems"
                };
                sqlCmd.Parameters.AddWithValue("@user_id", userId);              

                return await _dataReaderMapper.MapToListAsync<ItemInCart>(sqlCmd, map);
            }
            catch (Exception e)
            {
                // _logger.LogError("UserRepository:GetUserAUthoizedOptionsAsync:{0}", e.Message);
            }
            return null;
        }
    }
}
