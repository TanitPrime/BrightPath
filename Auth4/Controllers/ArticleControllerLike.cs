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
        //like
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(int? id)
        {

            var userName = _userManager.GetUserName(HttpContext.User);
            var likefound = await _context.LikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var dislikefound = await _context.DislikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var article= await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == id);
            var likeModel = new LikeModel();
            var url = "/Article/Details/" + id;
            if (likefound == null) { 
                if(dislikefound != null)
                {
                    await UnDislike(id);
                }
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

        //dislike
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dislike(int? id)
        {

            var userName = _userManager.GetUserName(HttpContext.User);
            var dislikefound = await _context.DislikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var likefound = await _context.LikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var article = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == id);
            var dislikeModel = new DislikeModel();
            var url = "/Article/Details/" + id;
            if (dislikefound == null)
            {
                if (likefound != null)
                {
                   await UnLike(id);
                }
                article.DislikeCount += 1;
                dislikeModel.ArticleId = article.ArticleId;
                dislikeModel.UserName = userName;
                _context.Add(dislikeModel);
                _context.Update(article);
                await _context.SaveChangesAsync();

                return LocalRedirect(url);

            }
            return LocalRedirect(url);

        }

        //unlike
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLike(int? id)
        {

            var userName = _userManager.GetUserName(HttpContext.User);
            var likeModel = await _context.LikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var article = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == id);
            
            var url = "/Article/Details/" + id;
            if (likeModel != null)
            {
                if (article.LikeCount >0)
                {
                    article.LikeCount -= 1;
                }
                
                
                _context.Remove(likeModel);
                _context.Update(article);
                await _context.SaveChangesAsync();

                return LocalRedirect(url);

            }
            return LocalRedirect(url);

        }


        //undislike
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnDislike(int? id)
        {

            var userName = _userManager.GetUserName(HttpContext.User);
            var dislikeModel = await _context.DislikeModels.FirstOrDefaultAsync(k => k.UserName == userName);
            var article = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == id);

            var url = "/Article/Details/" + id;
            if (dislikeModel != null)
            {
                if (article.DislikeCount > 0)
                {
                    article.DislikeCount -= 1;
                }
                

                _context.Remove(dislikeModel);
                _context.Update(article);
                await _context.SaveChangesAsync();

                return LocalRedirect(url);

            }
            return LocalRedirect(url);

        }
    }
}