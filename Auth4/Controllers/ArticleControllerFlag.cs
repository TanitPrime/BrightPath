using BrightPathDev.Models;
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
        //send the reports to flag list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Flag(int? CId, int? AId)
        {
            //declaring
            var userName = _userManager.GetUserName(HttpContext.User);
            var userId = _userManager.GetUserId(HttpContext.User);
            var flaggerExists = await _context.Flags.FirstOrDefaultAsync(k => k.FlaggerId == userId);
            var url = "/Article/Details/" + AId;

            Flag flag = new Flag();

            //if the user has a report in the db
            if (flaggerExists != null) { 
                //if his report was not about this article
                if (flaggerExists.ArticleId != AId && CId==null)
                {   
                    var article = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == AId);
                    article.FlagCount += 1;
                    flag.ArticleId = AId;
                    flag.FlaggerName = userName;
                    flag.FlaggerId = userId;
                    _context.Add(flag);
                    _context.Update(article);
                }//if his report was not about this comment
                else if (flaggerExists.CommentId !=CId )
                {
                    var comment = await _context.Comments.FirstOrDefaultAsync(k => k.CommentId == CId);
                    comment.FlagCount += 1;
                    flag.CommentId = CId;
                    flag.FlaggerName = userName;
                    flag.FlaggerId = userId;
                    _context.Add(flag);
                    _context.Update(comment);
                }
                //if the user has never reported before
            }else if (flaggerExists == null)
            {   //if the report was about a comment
                if (CId != null)
                {
                    var comment = await _context.Comments.FirstOrDefaultAsync(k => k.CommentId == CId);
                    comment.FlagCount += 1;
                    flag.CommentId = CId;
                    flag.FlaggerName = userName;
                    flag.FlaggerId = userId;
                    _context.Add(flag);
                    _context.Update(comment);
                }//if the report was about an article
                else if (AId != null && CId==null)
                {
                    var article = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == AId);
                    article.FlagCount += 1;
                    flag.ArticleId = AId;
                    flag.FlaggerName = userName;
                    flag.FlaggerId = userId;
                    _context.Add(flag);
                    _context.Update(article);
                }
            }


            await _context.SaveChangesAsync();
            return LocalRedirect(url);

            
             

        }
    }
}
