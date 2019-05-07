using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BrightPathDev.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrightPathDev
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {

                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();


                var adminRole = new IdentityRole("Admin");

                var rootRole = new IdentityRole("Root");


                if (!ctx.Roles.Any())
                {
                    //create a role
                    roleManager.CreateAsync(rootRole).GetAwaiter().GetResult();
                }
                if (!ctx.Users.Any(u => u.UserName == "Root"))
                {
                    //create a root 
                    var rootUser = new IdentityUser
                    {
                        UserName = "Root",
                        Email = "root@test.com"
                    };
                    var result = userManager.CreateAsync(rootUser, "password").GetAwaiter().GetResult();

                    //add role to user
                    userManager.AddToRoleAsync(rootUser, rootRole.Name).GetAwaiter().GetResult();

                    //create an admin
                    var adminUser = new IdentityUser
                    {
                        UserName = "Admin",
                        Email = "admin@test.com"
                    };

                    var result2 = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                    //add role to user
                    userManager.AddToRoleAsync(adminUser, rootRole.Name).GetAwaiter().GetResult();
                }
            }catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        private static int UserManager<T>()
        {
            throw new NotImplementedException();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
