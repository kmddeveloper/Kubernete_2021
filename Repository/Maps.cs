using Kubernetes.DataReaderMapper;
using EFModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernetes.Repository
{
    public class Maps: IMaps
    {
        readonly IDataReaderMapper _dataReaderMapper;

        public Maps(IDataReaderMapper dataReaderMapper)
        {
            _dataReaderMapper = dataReaderMapper;
        }
        public Dictionary<string, MapRule> UserMap
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                   .ForMember<User, Int32, SourceDataField>(d => d.Id, s => s.GetFieldName("id"))
                   .ForMember<User, String, SourceDataField>(d => d.UserName, s => s.GetFieldName("username"))
                   .ForMember<User, String, SourceDataField>(d => d.Password, s => s.GetFieldName("password"))
                   .ForMember<User, String, SourceDataField>(d => d.First_Name, s => s.GetFieldName("first_name"))
                   .ForMember<User, String, SourceDataField>(d => d.Last_Name, s => s.GetFieldName("last_name"))
                   .ForMember<User, String, SourceDataField>(d => d.Email, s => s.GetFieldName("email"))
                   .ForMember<User, String, SourceDataField>(d => d.Role, s => s.GetFieldName("category"));

                return map;
            }
        }

        public Dictionary<string, MapRule> SessionMap
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                   .ForMember<UserSession, String, SourceDataField>(d => d.Id, s => s.GetFieldName("id"))
                   .ForMember<UserSession, Int32, SourceDataField>(d => d.UserId, s => s.GetFieldName("user_id"))
                   .ForMember<UserSession, String, SourceDataField>(d => d.UserName, s => s.GetFieldName("username"))
                   .ForMember<UserSession, String, SourceDataField>(d => d.Password, s => s.GetFieldName("password"))
                   .ForMember<UserSession, String, SourceDataField>(d => d.First_Name, s => s.GetFieldName("first_name"))
                   .ForMember<UserSession, String, SourceDataField>(d => d.Last_Name, s => s.GetFieldName("last_name"))
                   .ForMember<UserSession, String, SourceDataField>(d => d.Email, s => s.GetFieldName("email"))
                   .ForMember<UserSession, String, SourceDataField>(d => d.Role, s => s.GetFieldName("category"));

                return map;
            }
        }

        public Dictionary<string, MapRule> ProductMap
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<Product, Int32, SourceDataField>(d => d.Id, s => s.GetFieldName("id"))
                  .ForMember<Product, String, SourceDataField>(d => d.Name, s => s.GetFieldName("name"))
                  .ForMember<Product, String, SourceDataField>(d => d.Description, s => s.GetFieldName("description"))
                  .ForMember<Product, String, SourceDataField>(d => d.Code, s => s.GetFieldName("code"))
                  .ForMember<Product, Decimal, SourceDataField>(d => d.Price, s => s.GetFieldName("price"))
                  .ForMember<Product, String, SourceDataField>(d => d.ImageUrl, s => s.GetFieldName("image_url"))
                  .ForMember<Product, Int32, SourceDataField>(d => d.StatusId, s => s.GetFieldName("status_id"))
                  .ForMember<Product, String, SourceDataField>(d => d.Status, s => s.GetFieldName("status"))
                  .ForMember<Product, String, SourceDataField>(d => d.Note, s => s.GetFieldName("note"))
                  .ForMember<Product, String, SourceDataField>(d => d.Category, s => s.GetFieldName("category"))
                  .ForMember<Product, Int32, SourceDataField>(d => d.CategoryId, s => s.GetFieldName("category_id"))
                  ;
                return map;
            }
        }


        public Dictionary<string, MapRule> ItemInCartMap
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ItemInCart, Int32, SourceDataField>(d => d.Id, s => s.GetFieldName("cart_id"))
                  .ForMember<ItemInCart, Int32, SourceDataField>(d => d.ProductId, s => s.GetFieldName("id"))
                  .ForMember<ItemInCart, Int32, SourceDataField>(d => d.ProductItemId, s => s.GetFieldName("product_item_id"))                  
                  .ForMember<ItemInCart, String, SourceDataField>(d => d.Name, s => s.GetFieldName("name"))
                  .ForMember<ItemInCart, String, SourceDataField>(d => d.Description, s => s.GetFieldName("description"))
                  .ForMember<ItemInCart, String, SourceDataField>(d => d.Code, s => s.GetFieldName("code"))
                  .ForMember<ItemInCart, String, SourceDataField>(d => d.Color, s => s.GetFieldName("color"))
                  .ForMember<ItemInCart, Decimal, SourceDataField>(d => d.Price, s => s.GetFieldName("price"))
                  .ForMember<ItemInCart, Double, SourceDataField>(d => d.MenSize, s => s.GetFieldName("size"))
                  .ForMember<ItemInCart, Double, SourceDataField>(d => d.WomenSize, s => s.GetFieldName("women_size"))
                  .ForMember<ItemInCart, String, SourceDataField>(d => d.ImageUrl, s => s.GetFieldName("image_url"))
                  .ForMember<ItemInCart, Int32, SourceDataField>(d => d.Quantity, s => s.GetFieldName("quantity"))
                  .ForMember<ItemInCart, Int32, SourceDataField>(d => d.AvailableQuantity, s => s.GetFieldName("avaiable_quantity"))
                  ;
                return map;
            }
        }

        public Dictionary<string, MapRule> ProductCategory
        {
            get
            {

                var map = _dataReaderMapper.CreateMap()
                 .ForMember<Field<int,string>, Int32, SourceDataField>(d => d.Key, s => s.GetFieldName("id"))
                 .ForMember<Field<int, string>, String, SourceDataField>(d => d.Value, s => s.GetFieldName("name"))
                 ;

                return map;
            }
        }

        public Dictionary<string, MapRule> ProductStatus
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                .ForMember<Field<int, string>, Int32, SourceDataField>(d => d.Key, s => s.GetFieldName("id"))
                .ForMember<Field<int, string>, String, SourceDataField>(d => d.Value, s => s.GetFieldName("description"))
                ;
                return map;
            }
        }



        public Dictionary<string, MapRule> ProductFeatures
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ProductFeature, Int32, SourceDataField>(d => d.ProductId, s => s.GetFieldName("product_id"))
                  .ForMember<ProductFeature, String, SourceDataField>(d => d.Feature, s => s.GetFieldName("feature"))
                            
                  ;
                return map;
            }
        }

        public Dictionary<string, MapRule> ProductSpec
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ProductSpecRaw, Int32, SourceDataField>(d => d.SpecId, s => s.GetFieldName("product_spec_id"))
                  .ForMember<ProductSpecRaw, String, SourceDataField>(d => d.Title, s => s.GetFieldName("header"))
                  .ForMember<ProductSpecRaw, String, SourceDataField>(d => d.ContentName, s => s.GetFieldName("name"))
                  .ForMember<ProductSpecRaw, String, SourceDataField>(d => d.ContentValue, s => s.GetFieldName("value"))
                  ;
                return map;
            }
        }

        public Dictionary<string, MapRule> ProductImages
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ProductImage, Int32, SourceDataField>(d => d.ProductId, s => s.GetFieldName("product_id"))
                  .ForMember<ProductImage, Int32, SourceDataField>(d => d.ProductItemId, s => s.GetFieldName("product_item_id"))
                  .ForMember<ProductImage, String, SourceDataField>(d => d.ImageUrl, s => s.GetFieldName("image_url"))
                  .ForMember<ProductImage, String, SourceDataField>(d => d.MediaType, s => s.GetFieldName("media_type"))
                  ;
                return map;
            }
        }

        public Dictionary<string, MapRule> ProductAttributes
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ProductAttribute, Int32, SourceDataField>(d => d.ProductId, s => s.GetFieldName("product_id"))
                  .ForMember<ProductAttribute, String, SourceDataField>(d => d.Attribute, s => s.GetFieldName("attribute"))

                  ;
                return map;
            }
        }

        public Dictionary<string, MapRule> ProductSizes
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ProductSize, Int32, SourceDataField>(d => d.Id, s => s.GetFieldName("size_id"))
                  .ForMember<ProductSize, decimal, SourceDataField>(d => d.MenSize, s => s.GetFieldName("size"))
                  .ForMember<ProductSize, decimal, SourceDataField>(d => d.WomenSize, s => s.GetFieldName("wsize"))
                  ;
                return map;
            }
        }

        public Dictionary<string, MapRule> ProductColors
        {
            get
            {
                var map = _dataReaderMapper.CreateMap()
                  .ForMember<ProductColor, Int32, SourceDataField>(d => d.Id, s => s.GetFieldName("color_id"))
                  .ForMember<ProductColor, string, SourceDataField>(d => d.Color, s => s.GetFieldName("name"))
                 
                  ;
                return map;
            }
        }
    }
}
