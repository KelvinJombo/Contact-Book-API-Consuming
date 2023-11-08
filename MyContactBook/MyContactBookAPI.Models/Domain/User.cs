using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Models.Domain
{
    public class User : IdentityUser
    {
        
        public string Email { get; set; }
                 
        public ICollection<Contact> Contacts { get; set; }
    }
}
