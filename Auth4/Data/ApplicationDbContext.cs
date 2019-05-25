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

        public DbSet<DeleteList> DeleteLists{ get; set; }

        public DbSet<FlagList> FlagLists{ get; set; }

        public DbSet<Image> Image { get; set; }
        public DbSet<LikeModel> LikeModels { get; set; }
        public DbSet<DislikeModel> DislikeModels { get; set; }
        public object Request { get; internal set; }
        public object LikeModel { get; internal set; }
        public object DislikeModel { get; internal set; }
    }
}
