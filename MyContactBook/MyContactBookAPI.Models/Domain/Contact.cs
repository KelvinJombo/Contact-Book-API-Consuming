using MyContactBookAPI.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Models.Domain
{
    public class Contact 
    {
        [Key]
        public string ContactId { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public bool isActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string? UserId { get; set; } 
        public User User { get; set; }
        public Gender Gender { get; set; }
        


    }
}
