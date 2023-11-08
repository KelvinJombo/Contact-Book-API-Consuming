using MyContactBookAPI.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Models.Dtos
{
    public class AddContactRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public bool isActive { get; set; }
        public Gender Gender { get; set; }
    }
}
