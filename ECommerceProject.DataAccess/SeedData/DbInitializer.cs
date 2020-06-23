

using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.DataAccess.SeedData
{
    public static class DbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           
            var userRole = new IdentityRole("User");
            userRole.NormalizedName = "USER";
            modelBuilder.Entity<IdentityRole>().HasData(userRole);
        }
    }
}
