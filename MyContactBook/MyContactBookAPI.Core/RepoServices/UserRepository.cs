using Microsoft.EntityFrameworkCore;
using MyContactBookAPI.Core.Interfaces;
using MyContactBookAPI.Data;
using MyContactBookAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.RepoServices
{
    public class UserRepository : IUserRepository
    {                
        private readonly MyContactDbContext myContactDbContext;

        public UserRepository(MyContactDbContext myContactDbContext)
        {
            this.myContactDbContext = myContactDbContext;
        }
         
        public async Task<User> CreateAsync(User user)
        {
             await myContactDbContext.Users.AddAsync(user);
            await myContactDbContext.SaveChangesAsync();
            return user;

        }

        public async Task<User?> DeleteAsync(string id)
        { 
             var existingUser = await myContactDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

                myContactDbContext.Remove(existingUser);
            await myContactDbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await myContactDbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
             return await myContactDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<User?> UpdateAsync(string id, User user)
        {
             var existingUser = await myContactDbContext.Users.FirstOrDefaultAsync(x =>x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;

            await myContactDbContext.SaveChangesAsync();
            return existingUser;

        }
    }
}
