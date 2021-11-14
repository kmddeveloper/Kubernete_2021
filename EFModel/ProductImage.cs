using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class ProductImage
    {
        public int ProductId { get; set; }
        public int ProductItemId { get; set; }
        public string ImageUrl { get; set; }
        public string MediaType { get; set; }
    }
}
