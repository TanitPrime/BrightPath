using BrightPathDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Controllers
{
    public partial class ArticleController : Controller
    {
        //add a comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(int? id, IFormCollection formFields)
        {
            //declaring
            var userName = _userManager.GetUserName(HttpContext.User);
            var userId = _userManager.GetUserId(HttpContext.User);
            var article = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == id);
            var comment = new Comment();
            var url = "/Article/Details/" + id;
           //assigning values
            comment.ArticleId = article.ArticleId;
            comment.UserName = userName;
            comment.CommentText = formFields["CommentText"];
            comment.FlagCount = 0;
            comment.UserId = userId;

            //saving
            await _context.AddAsync(comment);
            _context.SaveChanges();
                
            
            return LocalRedirect(url);
        }

        //delete a comment
        
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int? commenter,int? id)
        {
            var userName = _userManager.GetUserName(HttpContext.User);
            var comment = await _context.Comments.FirstOrDefaultAsync(k => k.CommentId == commenter);
            var flags = await _context.Flags.ToListAsync();
            var url = "/Article/Details/" + id;
            if (comment != null)
            {
                 _context.Remove(comment);
                foreach(var item in flags)
                {
                    if(item.CommentId == commenter)
                    {
                        _context.Remove(comment);
                    }
                }
                await _context.SaveChangesAsync();
                return LocalRedirect(url);
            }
            return LocalRedirect(url);
        }

    }
}
