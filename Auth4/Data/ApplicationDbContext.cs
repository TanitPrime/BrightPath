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

        public DbSet<Flag> Flags{ get; set; }

        public DbSet<Image> Image { get; set; }
        public DbSet<LikeModel> LikeModels { get; set; }
        public DbSet<DislikeModel> DislikeModels { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public object Flag { get; internal set; }
        public object Request { get; internal set; }
        public object LikeModel { get; internal set; }
        public object DislikeModel { get; internal set; }

        public object Comment { get; set; }
    }
}
