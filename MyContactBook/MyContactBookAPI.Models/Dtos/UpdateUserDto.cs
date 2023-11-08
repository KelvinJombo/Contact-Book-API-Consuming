using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Models.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Enter Minimum of 3 Characters")]
        [MaxLength(20, ErrorMessage = "Name must not contain more than 20 caracters")]
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
