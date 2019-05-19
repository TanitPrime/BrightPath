using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrightPathDev.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BrightPathDev
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {
            get;
            

        }

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
            services.AddDefaultIdentity<IdentityUser>(
                option =>
                {
                    //password options
                    option.Password.RequireDigit = false;
                    option.Password.RequiredLength = 6;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireLowercase = false;
                }
                )
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireRootRole", policy => policy.RequireRole("Root"));
            });

            services.AddMvc(config =>
            {
                //make everyone require to Authorize unless specified 
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Article}/{action=Index}/{id?}");
            });
            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Root", "Admin", "Muted" };
            IdentityResult roleResult;

            foreach(var roleName in roleNames)
            {
                //check if role exists
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and send to db
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            
            //creating root
            var poweruser = new IdentityUser
            {
                UserName = "Root",
                Email = "root@test.com",

            };
            //creating admin
            var adminuser = new IdentityUser
            {
                UserName = "Admin",
                Email = "admint@test.com",

            };
            string Pwd = "password";
            var _user = await UserManager.FindByEmailAsync("root@test.com");

            if(_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, Pwd);
                if (createPowerUser.Succeeded)
                {
                    //tie the poweruser to the role
                    await UserManager.AddToRoleAsync(poweruser, "Root");
                }
            }
             var _auser = await UserManager.FindByEmailAsync("admin@test.com");

            if (_auser == null)
            {
                var createAdminUser = await UserManager.CreateAsync(adminuser, Pwd);
                if (createAdminUser.Succeeded)
                {
                    //tie the admin to the role
                    await UserManager.AddToRoleAsync(adminuser, "Admin");
                }
            }

        }
    }
}
