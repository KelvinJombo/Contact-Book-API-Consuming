using MyContactBookAPI.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Models.Dtos
{
    public class ContactDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Enter Minimum of 3 Characters")]
        [MaxLength(20, ErrorMessage = "Name must not contain more than 20 caracters")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Enter Minimum of 3 Characters")]
        [MaxLength(20, ErrorMessage = "Name must not contain more than 20 caracters")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }         
        public string? Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool isActive { get; set; }
        public Gender Gender { get; set; }
        //public string UserId { get; set; }
    }
}
