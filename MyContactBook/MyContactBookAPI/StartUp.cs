using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace MyContactBookAPI
{
    public class StartUp
    {

        public async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("RegularUser"))
            {
                var regularUserRole = new IdentityRole("RegularUser");
                await roleManager.CreateAsync(regularUserRole);
            }

            var adminUser = await userManager.FindByNameAsync("Kelly");
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = "Kelly",
                    Email = "adminkelvin@mycontact.com"
                };

                await userManager.CreateAsync(adminUser, "Kelly@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var regularUser = await userManager.FindByNameAsync("Isibaby");
            if (regularUser == null)
            {
                regularUser = new IdentityUser
                {
                    UserName = "Isibaby",
                    Email = "regularoma@mycontact.com"
                };

                await userManager.CreateAsync(regularUser, "Isibaby@123");
                await userManager.AddToRoleAsync(regularUser, "RegularUser");
            }
        }

    }
}
