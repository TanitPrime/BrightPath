using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightPathDev.Models;
using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightPathDev.Controllers
{
    public partial class ArticleController : Controller
    {
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(int? id,LikeViewModel likeViewModel)
        {

            var userName = _userManager.GetUserName(HttpContext.User);
            var found = await _context.LikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var article= await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == id);
            var likeModel = new LikeModel();
            var url = "/Article/Details/" + id;
            if (found == null) { 
                article.LikeCount += 1;
                likeModel.ArticleId = article.ArticleId;
                likeModel.UserName = userName;
                _context.Add(likeModel);
                _context.Update(article);
                await _context.SaveChangesAsync();
                
                return LocalRedirect(url);
                
            }
            return LocalRedirect(url);
           
        }
    }
}