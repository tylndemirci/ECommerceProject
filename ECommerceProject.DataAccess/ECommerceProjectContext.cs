using ECommerceProject.Entities;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.DataAccess
{
    public class ECommerceProjectContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceProjectContext(DbContextOptions<ECommerceProjectContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
      
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}
