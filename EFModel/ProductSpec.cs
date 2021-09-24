using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{
    public class ProductSpec
    {
        public string Title { get; set; }
        public List<Field<String,String>> Fields { get; set; }
    }
}
