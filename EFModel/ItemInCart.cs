using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFModel
{
    public class ItemInCart
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        [MaxLength(16)]
        public string Code { get; set; }

        [Required]
        [MaxLength(256)]
        public string ImageUrl { get; set; }

        [Range(0, 1000000)]
        public decimal Price { get; set; }     

        public int Quantity { get; set; }
    }
}
