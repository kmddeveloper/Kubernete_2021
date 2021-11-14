using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFModel
{
    public class CartUpdateFields
    {
        [Required]        
        public string UserId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int CartId { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }
    }
}
