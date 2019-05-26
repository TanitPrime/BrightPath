using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
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


        // GET: Article/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id,ArticleViewModel articleViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            articleViewModel.Article = await _context.Articles.FindAsync(id);
            var likes = _context.LikeModels.ToList();
            var dislikes = _context.DislikeModels.ToList();
            var comments = _context.Comments.ToList();
            articleViewModel.LikeModels = likes;
            articleViewModel.DislikeModels = dislikes;
            articleViewModel.Comments = comments;
            if (articleViewModel.Article == null)
            {
                return NotFound();
            }

            return View(articleViewModel);
        }
    }
}
