using BrightPathDev.Models;
using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Controllers
{
    public partial class ArticleController : Controller
    {
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


                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", userId, article.ArticleTitle);
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

    }
}
