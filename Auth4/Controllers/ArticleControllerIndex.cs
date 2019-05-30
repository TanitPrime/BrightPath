using BrightPathDev.Data;
using BrightPathDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Controllers
{
    public partial class ArticleController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHostingEnvironment _hostingEnvironment;


        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }


        // GET: Article search
        [AllowAnonymous]
        public ActionResult Search(string searching)
        {
            return View("Index", _context.Articles.Where(x => x.ArticleTitle.Contains(searching) || searching == null).ToList());
        }
        // GET: Article
        [AllowAnonymous]
        public ActionResult Index()
        {
            var articlelist = _context.Articles.ToList();
            var list = new List<Article>();
            var mostB = 0;
            var mostBid = 0;
            var mostE = 0;
            var mostEid = 0;
            var mostH = 0;
            var mostHid = 0;
            foreach (var item in articlelist)
            {
                if (item.Category=="Business")
                {
                    if (item.LikeCount > mostB)
                    {
                        mostB = item.LikeCount;
                        mostBid = item.ArticleId;
                    }
                    
                }
                if (item.Category == "Entertainment")
                {
                    if (item.LikeCount > mostE)
                    {
                        mostE = item.LikeCount;
                        mostEid = item.ArticleId;
                    }

                }
                if (item.Category == "Health")
                {
                    if (item.LikeCount > mostH)
                    {
                        mostH = item.LikeCount;
                        mostHid = item.ArticleId;
                    }

                }
            }
            if (mostBid != 0)
            {
                list.Add(articlelist.First(item => item.ArticleId == mostBid));
            }
            if (mostEid != 0)
            {
                list.Add(articlelist.First(item => item.ArticleId == mostEid));
            }
            if (mostHid != 0)
            {
                list.Add(articlelist.First(item => item.ArticleId == mostHid));
            }


            if (list.Count >0)
            {
                return View("TopPicks", list);
            }
            else
            {
                return View("Index", articlelist);
            }
        }
        [AllowAnonymous]
        public ActionResult CategoryIndex(string category)
        {
            return View("Index", _context.Articles.Where(x => x.Category ==category));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
