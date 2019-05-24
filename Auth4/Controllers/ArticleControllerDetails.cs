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
        public async Task<IActionResult> Details(int? id,LikeViewModel likeViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            likeViewModel.Article = await _context.Articles.FindAsync(id);
            var likes = _context.LikeModels.ToList();
            likeViewModel.LikeModels = likes;
            if (likeViewModel.Article == null)
            {
                return NotFound();
            }

            return View(likeViewModel);
        }
    }
}
