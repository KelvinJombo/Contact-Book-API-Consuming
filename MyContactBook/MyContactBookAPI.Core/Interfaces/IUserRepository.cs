﻿using MyContactBookAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.Interfaces
{
    public interface IUserRepository
    {
       Task<List<User>>GetAllAsync();


        Task<User?>GetByIdAsync(string id);


        Task<User>CreateAsync(User user);


        Task<User?> UpdateAsync(string id, User user);

        Task<User?> DeleteAsync(string id);
    }


}
