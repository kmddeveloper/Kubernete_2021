using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class ShoppingCart
    {
        public List<ItemInCart> ItemsInCart { get; set; }
        public decimal Count { get; set; }
        public decimal Total { get; set; }
    }
}
