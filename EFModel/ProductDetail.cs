using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class ProductDetail
    {
        public Product Item { get; set; }        
        public List<ProductImage> ItemImages { get; set; }
        public List<ProductFeature> Features { get; set; }
        public List<ProductSpec> Specs { get; set; }
        public List<ProductAttribute> Attributes { get; set; }
    }
}
