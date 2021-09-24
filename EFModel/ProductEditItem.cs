using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class ProductEditItem
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
      
        public string Description { get; set; }

        public string Code { get; set; }
     
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public int CategoryId { get; set; }
        public string Note { get; set; }

        public List<DropdownField<int, string>> CategoryList { get; set; }
        public List<DropdownField<int, string>> StatusList { get; set; }
    }
}
