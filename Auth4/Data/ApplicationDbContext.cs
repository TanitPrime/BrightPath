using System;
using System.Collections.Generic;
using System.Text;
using BrightPathDev.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrightPathDev.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Article> Articles { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Image> Image { get; set; }

    }
}
