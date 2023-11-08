using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyContactBookAPI.Models.Domain;

namespace MyContactBookAPI.Data
{
    public class MyContactDbContext : IdentityDbContext<User>
    {
        public MyContactDbContext(DbContextOptions<MyContactDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRoles(builder);

        }
        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasKey(x => x.Id);
            builder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Id = "1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                  new IdentityRole() { Id = "2", Name = "Regular", ConcurrencyStamp = "2", NormalizedName = "Regular" }
                );
        }


    }   
}