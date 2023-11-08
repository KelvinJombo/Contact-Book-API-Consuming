using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyContactBookAPI.Core.Interfaces;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.RepoServices
{
    public class Register : IRegister
    {
        
        private readonly UserManager<User> _userManager;         
        private readonly RoleManager<IdentityRole> _roleManager;

        public Register(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> RegisterUserAsync(RegisterRequestDto model, ModelStateDictionary modelState, string role)
        {
            if (!modelState.IsValid)
            {
                return false;
            }
            else
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                if (await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            modelState.AddModelError(string.Empty, error.Description);
                        }
                        return false;
                    }
                    await _userManager.AddToRoleAsync(user, role);
                    return true;

                }
                return false;

            }

        }
    }
    
}
