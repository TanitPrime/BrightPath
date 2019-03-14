using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightPath.Models;

namespace BrightPath.Models
{
    public class BrightContext : DbContext
    {
        public BrightContext(DbContextOptions<BrightContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Image> Image { get; set; }

        

    }
}
