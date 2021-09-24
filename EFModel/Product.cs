using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFModel
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }      

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

        [Range(1, 100000)]
        public int StatusId { get; set; }

        [MaxLength(24)]
        public string Status { get; set; }

        [Range(1, 100000)]
        public int CategoryId { get; set; }

        [MaxLength(64)]
        public string Category { get; set; }

        [Required]
        [MaxLength(128)]
        public string Note { get; set; }
    }
}
