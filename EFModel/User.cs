using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFModel
{
    public class User
    {
        [Key]                
        public int Id { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        public string First_Name { get; set; }

        [Required]
        [MaxLength(64)]
        public string Last_Name { get; set; }

        public string Role { get; set; }

    }
}
