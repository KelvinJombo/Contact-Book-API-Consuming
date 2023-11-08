using MyContactBook.UI.MyContactBookUI.Commons;
using System.ComponentModel.DataAnnotations;

namespace MyContactBook.UI.Models.DTO
{
    public class ContactPutDto
    {
        public string ContactId { get; set; }  
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
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
    }
}
