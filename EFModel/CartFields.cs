using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFModel
{
    public class CartFields
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int ProductId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int ProductItemId { get; set; }
        [Range(1, Int32.MaxValue)]
        [Required]
        public int Quantity { get; set; }
   
        [Required]
        public string UserId { get; set; }


    }
}
