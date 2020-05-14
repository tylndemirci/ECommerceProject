using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Business.Concrete;
using ECommerceProject.DataAccess;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.DataAccess.Concrete;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerceProject.AdminUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ECommerceProjectContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly("ECommerceProject.AdminUI"))
                );

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = false;
                    
                }).AddEntityFrameworkStores<ECommerceProjectContext>()
                .AddDefaultTokenProviders();
            //services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Member/Login");
            services.AddTransient<IProductDal, EfProductDal>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<ICategoryDal, EfCategoryDal>();
            services.AddTransient<ICategoryService, CategoryManager>();
          
           
            

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AdminPage}/{action=Index}/{id?}");
            });
        }
    }
}
