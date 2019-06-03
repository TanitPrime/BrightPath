using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BrightPathDev.Data;
using BrightPathDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrightPathDev.Areas.Identity.Pages.Account
{

    public class MyPosts
    {
        public string UserName { get; set; }

        public string Title { get; set; }
        public string DateOfRequest { get; set; } 
        public int ArticleId { get; set; }

        public ContactStatus Status { get; set; }

    }
    [Authorize]
    public class MyArticles : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MyArticles(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }



        public IList<MyPosts> rqs = new List<MyPosts>();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            
            var rq = _context.Articles.ToList();
            foreach(var u in rq)
            {
                if(u.AuthorId == userId)
                {
                    MyPosts X = new MyPosts
                    {
                        ArticleId = u.ArticleId,
                        Title = u.ArticleTitle,
                        UserName = u.AuthorName,
                        DateOfRequest = DateTime.Now.ToString().ToString(),
                        Status = u.Status
                    };
                    rqs.Add(X);

                }
                
            }



        }




    }
}
