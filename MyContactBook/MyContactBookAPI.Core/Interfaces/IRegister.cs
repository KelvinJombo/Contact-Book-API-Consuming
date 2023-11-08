using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyContactBookAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.Interfaces
{
    public interface IRegister
    {
        public Task<bool> RegisterUserAsync(RegisterRequestDto model, ModelStateDictionary modelState, string role);
    }
}
