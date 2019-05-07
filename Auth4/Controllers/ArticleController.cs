using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrightPathDev.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BrightPathDev.ViewModels;
using BrightPathDev.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace BrightPathDev.Controllers
{




    public class ArticleController : Controller
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


        // GET: Article

        public ActionResult Index(string searching)
        {
            return View(_context.Articles.Where(x => x.ArticleTitle.Contains(searching) || searching == null).ToList());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(article);
        }

        // GET: Article/Create
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,ArticleTitle,desc_mini,desc,ArticleAdress,Articlecoor,ArticleContact,ImagePath")] Article article, ViewModelBoth viewModelBoth)
        {
            if (ModelState.IsValid)
            {
                //id stuff
                
                var userId = _userManager.GetUserId(HttpContext.User);
                var userName = _userManager.GetUserName(HttpContext.User);
                article.AuthorId = userId;
                article.AuthorName = userName;

                //img upload stuff
                
               
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", userId,article.ArticleTitle);
                    Directory.CreateDirectory(uploads);

                    



                foreach (var file in viewModelBoth.Files)
                {
                    if (file.Length > 0)
                    {
                        //  TODO: change the filename so it doesnt save the original user one, could be malicious or bad idea

                        //making the img name
                        string filename = $"{article.ArticleTitle}{DateTime.Now.ToString("ssddmmyyyy")}{Path.GetExtension(file.FileName)}";
                       
                        //assigning values
                        article.ImageName = filename;
                        article.ImagePath = uploads;
                        //directing path for file
                        var filePath = Path.Combine(uploads, filename);
                       
                        //streaming file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            _context.Add(article);
                            
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        private object CreateWebHostBuilder(object args)
        {
            throw new NotImplementedException();
        }

        // GET: Article/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id, ViewModelBoth viewModelBoth)
        {
            if (id == null)
            {
                return NotFound();
            }

            viewModelBoth.Article = await _context.Articles.FindAsync(id);
            if (viewModelBoth.Article == null)
            {
                return NotFound();
            }
            return View(viewModelBoth);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,ArticleTitle,desc_mini,desc,ArticleAdress,Articlecoor,ArticleContact,ImagePath")] Article article, ViewModelBoth viewModelBoth)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", userId, article.ArticleTitle);

                    foreach (var file in viewModelBoth.Files)
                    {
                        if (file.Length > 0)
                        {
                            //  TODO: change the filename so it doesnt save the original user one, could be malicious or bad idea

                            //making the img name
                            string filename = $"{article.ArticleTitle}{DateTime.Now.ToString("ssddmmyyyy")}{Path.GetExtension(file.FileName)}";

                            //assigning values
                            article.ImageName = filename;
                            article.ImagePath = uploads;
                            //directing path for file
                            var filePath = Path.Combine(uploads, filename);

                            //streaming file
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                        }
                    }
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Article/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
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

            return View(article);
        }

        // POST: Article/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            var userId = _userManager.GetUserId(HttpContext.User);
            
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", userId, article.ArticleTitle);
            if (Directory.Exists(uploads))
            {
                Directory.Delete(uploads,true);
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
