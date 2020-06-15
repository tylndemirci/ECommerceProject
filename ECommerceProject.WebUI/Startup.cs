using ECommerceProject.Business.Abstract;
using ECommerceProject.Business.Concrete;
using ECommerceProject.DataAccess;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.DataAccess.Concrete;
using ECommerceProject.Entities;
using ECommerceProject.WebUI.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ECommerceProject.WebUI
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
            services.AddDbContext<ECommerceProjectContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {

                })
                .AddEntityFrameworkStores<ECommerceProjectContext>()
                .AddDefaultTokenProviders();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICategoryDal, EfCategoryDal>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<IProductDal, EfProductDal>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<ICartService, CartManager>();
            services.AddTransient<ICartSessionHelper, CartSessionHelper>();
            services.AddTransient<IOrderService, OrderManager>();
            services.AddTransient<IOrderDal, EfOrderDal>();
            services.AddTransient<IProductDetailsDal, EfProductDetailsDal>();
            services.AddTransient<IProductDetailsService, ProductDetailsManager>();



            services.AddAuthentication("CookieAuth").AddCookie(opt =>
            {
                opt.Cookie.Name = "Ecom";
                opt.LoginPath = "/Account/Login";
            });
           
            services.AddControllersWithViews();
            services.AddCloudscribePagination();
            services.AddMemoryCache();
            services.AddSession();
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
            app.UseSession();
            
            app.UseAuthentication();

            app.UseAuthorization();

          
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

           

        }
    }
}
