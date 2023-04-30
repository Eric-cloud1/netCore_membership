using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MemberApi.Data;
using MemberApi.Models;
using MemberApi.Models.AccountViewModels;
using MemberApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using MemberApi.Infrastructure;
using Microsoft.AspNetCore.Http;
using MemberApi.Models.Services;

namespace MemberApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IPasswordValidator<LoginViewModel>,  CustomPasswordValidator>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            // Add application services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<MenuMasterService, MenuMasterService>();
            services.AddTransient<ProfileOptionsService, ProfileOptionsService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMemoryCache();
            services.AddSession();

            //Password Strength Setting  
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings  
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                //    // Lockout settings  
                //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                //    // User settings  
                options.User.RequireUniqueEmail = true;
            });

            ////Seting the Account Login page  
            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings  
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login  
            //    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout  
            //    options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied  
            //    options.SlidingExpiration = true;
            //});



            //services.AddMvc();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // [Authorize(Policy = "ApprovedDispensary")]
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApprovedDispensary", policy =>
            //      policy.RequireRole("Dispensary", "Approved"));
            //    options.AddPolicy("RegisteredDispensary", policy =>
            //  policy.RequireRole("Registered", "Approved"));
            //});

        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => {

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(name: "Error", template: "Error",
                    defaults: new { controller = "Error", action = "Error" });

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                );

                routes.MapRoute(
                       name: "pagination",
                       template: "Products/Page{page}",
                       defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(
                 name: null,
                 template: "",
                 defaults: new { controller = "Product", action = "Edit" });

                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/{id?}");

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });


            SeedData.EnsurePopulated(services);
            CreateUserRoles(services).Wait();
        }


        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            IdentityResult roleResult;
            //create the roles and seed them to the database  
            //Admin, Dispensary, TestLab and Processor

            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("GoldMember");
            if (!roleCheck)
            { 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("GoldMember"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("SilverMember");
            if (!roleCheck)
            { 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("SilverMember"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("BronzeMember");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("BronzeMember"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("Registered");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Registered"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("Approved");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Approved"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("Lockout");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Lockout"));
            }
          
                //Assign Admin role to the main User for Admin management  
                var user = await UserManager.FindByEmailAsync("admin@outlook.com");
            if (user is null)
            {
                user = new ApplicationUser();
                user.Email = "admin@outlook.com";
                user.UserName = "admin@outlook.com";
                var result = await UserManager.CreateAsync(user, "P@ssw0rd");
               
            }

            await UserManager.AddToRoleAsync(user, "Admin");

            var contact =  new MemberApi.Models.Members.Contact();
            contact.Email = "admin@outlook.com";

         

            
        }


    }
}
