using Microsoft.AspNetCore.Identity;
using MyContactBookAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.Interfaces
{
    public interface IAuthServices
    {
         string CreateJWT(IdentityUser user, List<string> roles);

    }
}
