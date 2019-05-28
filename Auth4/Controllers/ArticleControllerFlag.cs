using BrightPathDev.Models;
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
        //flag an article
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FlagArticle(int? AId)
        {
            //declaring
            var userName = _userManager.GetUserName(HttpContext.User);
            var userId = _userManager.GetUserId(HttpContext.User);
            var flagsfromdb = _context.Flags.ToList();
            
            var url = "/Article/Details/" + AId;

            Flag flag = new Flag();
            bool x = false;
            foreach (var item in flagsfromdb)
            {
                x = false;
                if (item.ArticleId == AId && item.FlaggerId == userId && item.CommentId==null)
                {
                    x = true;
                }
            }
            if (x == false)
            {
                flag.ArticleId = AId;
                flag.FlaggerId = userId;
                flag.FlaggerName = userName;
                var article = _context.Articles.FirstOrDefault(k => k.ArticleId == AId);
                article.FlagCount += 1;
                flag.CommentText = "";
                await _context.AddAsync(flag);
            }


            await _context.SaveChangesAsync();
            return LocalRedirect(url);

            
             

        }
        //flag a comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FlagComment(int? CId,int? AId)
        {
            //declaring
            var userName = _userManager.GetUserName(HttpContext.User);
            var userId = _userManager.GetUserId(HttpContext.User);
            var flagsfromdb = _context.Flags.ToList();
            
            var url = "/Article/Details/" + AId;

            Flag flag = new Flag();
            bool x = false;
            foreach (var item in flagsfromdb)
            {
                x = false;
                if(item.CommentId == CId && item.FlaggerId == userId)
                {
                    x = true;
                }    
            }
            if (x == false)
            {
                flag.ArticleId = AId;
                flag.CommentId = CId;
                flag.FlaggerId = userId;
                flag.FlaggerName = userName;
                var comment = _context.Comments.FirstOrDefault(k => k.CommentId == CId);
                comment.FlagCount += 1;
                flag.CommentText = comment.CommentText;
                await _context.AddAsync(flag);
            }
            await _context.SaveChangesAsync();
            return LocalRedirect(url);




        }


    }
}
