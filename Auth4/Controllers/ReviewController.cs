using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightPathDev.Data;
using BrightPathDev.Models;
using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth4.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ReviewController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> ReviewRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }
            
            return View("../Article/DeleteRequest", article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToList([Bind("DListId,ArticleId,AuthorId,AuthorName,Reason,DateOfRequest")]DeleteList deleteList,int id, IFormCollection formFields)
        {

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }
            deleteList.AuthorId = article.AuthorId;
            deleteList.ArticleId = article.ArticleId;
            deleteList.AuthorName = article.AuthorName;
            deleteList.DateOfRequest = $"{DateTime.Now.ToString("ssddmmyyyy")}";
            deleteList.Reason = formFields["reason"];

            var poop = "pee";
            _context.Update(deleteList);
            await _context.SaveChangesAsync();



            return LocalRedirect("/");
        }
    }
}