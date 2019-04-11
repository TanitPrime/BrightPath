using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrightPath.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BrightPath.ViewModels;

namespace BrightPath.Controllers
{

   


public class ArticleController : Controller
    {


        private readonly IHostingEnvironment _hostingEnvironment;

       
        private readonly BrightContext _context;

        public ArticleController(BrightContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
    

        // GET: Article
        
        public  ActionResult Index(string searching )
        {
            return View( _context.Articles.Where(x => x.ArticleTitle.Contains(searching) || searching == null).ToList()); 
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,ArticleTitle,desc_mini,desc,ArticleAdress,Articlecoor,ArticleContact,ImagePath")] Article article, ViewModelBoth viewModelBoth)
        {
            if (ModelState.IsValid)
            {
                
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
                foreach (var file in viewModelBoth.Files)
                {
                    if (file.Length > 0)
                    {
                        //  TODO: change the filename so it doesnt save the original user one, could be malicious or bad idea


                        string filename = $"{article.ArticleTitle}{DateTime.Now.ToString("ssddmmyyyy")}{Path.GetExtension(file.FileName)}";
                       // string filename = article.ArticleTitle + Path.GetExtension(file.FileName);
                       // filename = filename + DateTime.Now.ToString("yymmssfff");
                        article.ImagePath = filename;
                        var filePath = Path.Combine(uploads, filename);
                        

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            _context.Add(article);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                


            }
            return View(article);
        }

        // GET: Article/Edit/5
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

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
                    foreach (var file in viewModelBoth.Files)
                    {
                        if (file.Length > 0)
                        {


                            //making file name
                            string filename = $"{article.ArticleTitle}{DateTime.Now.ToString("ssddmmyyyy")}{Path.GetExtension(file.FileName)}";
                            //assigning file name
                           // System.Diagnostics.Debug.WriteLine("3333333333333333333333333333333333333333333333333" + filename);
                            article.ImagePath = filename;
                            //sending file in this filepath
                            var filePath = Path.Combine(uploads, filename);

                            //streaming the file down
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
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
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
