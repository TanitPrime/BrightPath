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

    public class FG
    {
        public string FlaggerName { get; set; }
        public int? ArticleId { get; set; }

        public int? CommentId { get; set; }
        public string CommentText { get; set; }

        public int FlagId { get; set; }

    }
    [Authorize(Roles ="Root,Admin")]
    public class ManageFlagsModel : PageModel
    {

 
        private readonly ApplicationDbContext _context;

        public ManageFlagsModel(
            UserManager<IdentityUser> userManager,

            ApplicationDbContext context)
        {

            _context = context;

        }


        
        public IList<FG> FGs = new List<FG>();

        public async Task OnGetAsync()
        {
            var flags = _context.Flags.ToList();
            FGs.Clear();
            foreach (var u in flags)
            {
                FG X = new FG
                {
                    FlaggerName = u.FlaggerName,
                    ArticleId = u.ArticleId,
                    CommentId = u.CommentId,
                    CommentText = u.CommentText,
                    FlagId = u.FlagId
                };
                FGs.Add(X);
            }
            



        }
        
        public IActionResult OnPostRemoveFlag()
        {
            //declaring

            var flagsfromdb = _context.Flags.ToList();


            foreach (var item in flagsfromdb)
            {

                if (item.CommentId == null && item.ArticleId== null)
                {
                    _context.Remove(item);
                }
            }

             _context.SaveChangesAsync();
            //return LocalRedirect("Identity/Account/Manage/ManageFlags");


            return Page();
        }


    }
}
