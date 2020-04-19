using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.DataAccess
{
    public class ECommerceProjectContext : DbContext
    {
        public ECommerceProjectContext(DbContextOptions<ECommerceProjectContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
