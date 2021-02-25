using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Domain;
using PizzaShopApplication.Models.Domain.Interfaces;
using PizzaShopApplication.Models.Secondary;

namespace PizzaShopApplication
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
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddTransient<IProduct, ShowProductRepository>();
            services.AddTransient<EditProductDataRepository>();
            services.AddTransient<ShoppingCartRepository>();
            services.AddTransient<IOrder, OrderRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDataContext>(options => options.UseSqlServer(connection));
            // Установка конфигурации подключения.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            // Посредством аутентификации мы идентифицируем пользователя, узнаем, кто он.
            app.UseAuthentication();
            // Авторизация отвечает на вопрос, какие права в системе имеет пользователь, 
            // позволяет разграничить доступ к ресурсам приложения.
            app.UseAuthorization();
            // Т.к. будет использоваться модель машрутизации на основе атрибутов,
            // то не определяем никаких других маршрутов.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseAuthorization();
            
        }
    }
}
