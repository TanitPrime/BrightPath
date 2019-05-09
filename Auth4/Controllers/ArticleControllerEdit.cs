using BrightPathDev.Models;
using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Controllers
{
    public partial class ArticleController : Controller
    {
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

                    var x = article.AuthorId;
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", x, article.ArticleTitle);

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
    }
}
