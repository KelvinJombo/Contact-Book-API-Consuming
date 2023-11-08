using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace MyContactBookAPI.Commons
{
    //public class SeedData
    //{
    //    public static void Initialize(IServiceProvider serviceProvider)
    //    {
    //        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    //        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //        // Ensure that roles exist
    //        if (!roleManager.RoleExistsAsync("Admin").Result)
    //        {
    //            var adminRole = new IdentityRole("Admin");
    //            roleManager.CreateAsync(adminRole).Wait();
    //        }

    //        // Ensure that an admin user exists
    //        if (userManager.FindByNameAsync("admin").Result == null)
    //        {
    //            var adminUser = new User
    //            {
    //                UserName = "admin",
    //                Email = "admin@example.com"
    //            };

    //            var result = userManager.CreateAsync(adminUser, "adminPassword").Result;
    //            if (result.Succeeded)
    //            {
    //                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
    //            }
    //        }
    //    }
    //}
}
