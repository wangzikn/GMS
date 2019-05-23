using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMS.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GMS.Data.Interfaces;
using GMS.Data.Responsitories;
using GMS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GMS
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<Individual>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddTransient<ApplicationDbContext, ApplicationDbContext>();
            //services.AddScoped<IAreaResponsitory, AreaResponsitory>();
            services.AddScoped<IIndividualResponsitory, IndividualResponsitory>();
            services.AddScoped<IAccountModelResponsitory, AccountModelResponsitory>();
            services.AddScoped<IAreaRepository, AreaResponsitory>();
            services.AddScoped<IRoleResponsitory, RoleReponsitory>();
            services.AddAuthentication().AddCookie((x) =>
            {
                x.LoginPath = "/Account/Login";
                x.LogoutPath = "/Account/Logout";
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer db, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            db.Initialize().Wait();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
                routes.MapRoute(
                   name: "api",
                   template: "api/{controller}/{action}");
            });
            // CreateAdminUser(serviceProvider).Wait();
        }

        //private async Task CreateAdminUser(IServiceProvider serviceProvider)
        //{
        //    var UserManager = serviceProvider.GetRequiredService<UserManager<Individual>>();
        //    //creating a super user who could maintain the web app
        //    Individual adminUser = new Individual();
        //    adminUser.UserName = "Admin";
        //    adminUser.Email = "admin@gmail.com";            
        //    string UserPassword = "Admin@123*";
        //    Individual _user = await UserManager.FindByEmailAsync(adminUser.Email);​
        //    if (_user == null)
        //    {
        //        var createAdminUser = await UserManager.CreateAsync(adminUser, UserPassword);
        //    }

        //    // creating all roles for adminUser: them tat ca cac quyen tren manf hinh giao dien phan quyen cho adminuser
        //}
    }
}
