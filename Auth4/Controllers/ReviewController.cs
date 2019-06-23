using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BrightPathDev.Data;
using BrightPathDev.Models;
using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Auth4.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReviewController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> ReviewRequest(int? id)
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

            return View("../Article/DeleteRequest", article);
        }

        public async Task<IActionResult> EditRequest(int? id, EditList editList)
        {
            if (id == null)
            {
                return NotFound();
            }

            editList.ViewModelBoth.Article = await _context.Articles.FindAsync(id);
            if (editList.ViewModelBoth.Article == null)
            {
                return NotFound();
            }
            return View("../Article/EditRequest",editList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToList([Bind("DListId,ArticleId,AuthorId,AuthorName,Reason,DateOfRequest")]DeleteList deleteList, int id, IFormCollection formFields)
        {


            //find the article by id
            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            //grab the delete list from db where Author.Id == Author.Id
            var x = await _context.DeleteLists.FirstOrDefaultAsync(m => m.AuthorId == article.AuthorId);
            //inserting values into the deletelist
            if (x == null)
            {
                deleteList.AuthorId = article.AuthorId;
                deleteList.ArticleId = article.ArticleId;
                deleteList.AuthorName = article.AuthorName;
                deleteList.DateOfRequest = $"{DateTime.Now.ToString("ssddmmyyyy")}";
                deleteList.Reason = formFields["reason"];

                //updating
                _context.Update(deleteList);
                await _context.SaveChangesAsync();



                return LocalRedirect("/");
            }
            else
            {
                return View("../Article/RequestDenied");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            //find the article by id
            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }


            //approve it
            article.Status = ContactStatus.Approved;



            _context.Update(article);
            await _context.SaveChangesAsync();

            return LocalRedirect("/");



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdited([Bind("ArticleId,ArticleTitle,desc_mini,desc,ArticleAdress,Articlecoor,ArticleContact,ImagePath,ImageName,AuthorId,AuthorName,FlagCount,Status")] Article article, EditList editList,int Id, IFormCollection formFields)
        {
            
                //id stuff
                editList.OriginalId = Id;

                var userId = _userManager.GetUserId(HttpContext.User);
                var userName = _userManager.GetUserName(HttpContext.User);
                editList.ViewModelBoth.Article.AuthorId = userId;
                editList.ViewModelBoth.Article.AuthorName = userName;
                editList.ViewModelBoth.Article.Status = ContactStatus.Edited;
                //img upload stuff


                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", userId, "Edited");
                Directory.CreateDirectory(uploads);





                foreach (var file in editList.ViewModelBoth.Files)
                {
                    if (file.Length > 0)
                    {
                        //  TODO: change the filename so it doesnt save the original user one, could be malicious or bad idea

                        //making the img name
                        string filename = $"{editList.ViewModelBoth.Article.ArticleTitle}{DateTime.Now.ToString("ssddmmyyyy")}{Path.GetExtension(file.FileName)}";

                    //assigning values
                        editList.ViewModelBoth.Article.ImageName = filename;
                        editList.ViewModelBoth.Article.ImagePath = uploads;
                        //directing path for file
                        var filePath = Path.Combine(uploads, filename);

                        //streaming file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            _context.Add(editList);

                        }

                    }
                }
                await _context.SaveChangesAsync();
                //renaming directory to a more secure one
                //var newid = editList.ViewModelBoth.Article.ArticleId.ToString();
                //var SecUploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", userId, newid);
                //Directory.Move(uploads, SecUploads);
                

            
            return LocalRedirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveEdit(int id, [Bind("ArticleId,ArticleTitle,desc_mini,desc,ArticleAdress,Articlecoor,ArticleContact,ImagePath,ImageName,AuthorId,AuthorName,FlagCount,Status")] Article article, EditList editList)
        {

            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    //grab the article straight from db
                    var articleFromDb = await _context.Articles.FirstOrDefaultAsync(k => k.ArticleId == article.ArticleId);
                    var authorId = _context.Articles.FirstOrDefault(x => x.ArticleId == article.ArticleId).AuthorId;


                    //assign the articlefromdb properties to what the user edited
                    articleFromDb.ArticleTitle = article.ArticleTitle;
                    articleFromDb.ArticleAdress = article.ArticleAdress;
                    articleFromDb.ArticleContact = article.ArticleContact;
                    articleFromDb.desc = article.desc;
                    articleFromDb.desc_mini = article.desc_mini; ;
                    articleFromDb.ArticleLat = article.ArticleLat;
                    articleFromDb.ArticleLng = article.ArticleLng;
                    articleFromDb.Status = ContactStatus.Approved;


                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", authorId, articleFromDb.ArticleId.ToString());

                    foreach (var file in editList.ViewModelBoth.Files)
                    {
                        if (file.Length > 0)
                        {
                            //  TODO: change the filename so it doesnt save the original user one, could be malicious or bad idea

                            //making the img name
                            string filename = $"{article.ArticleTitle}{DateTime.Now.ToString("ssddmmyyyy")}{Path.GetExtension(file.FileName)}";

                            //assigning values
                            articleFromDb.ImageName = filename;
                            articleFromDb.ImagePath = uploads;
                            //directing path for file
                            var filePath = Path.Combine(uploads, filename);

                            //streaming file
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                        }
                    }
                    //put back the articlefromdb back to db
                    _context.Update(articleFromDb);
                    await _context.SaveChangesAsync();
                    var newid = editList.ViewModelBoth.Article.ArticleId.ToString();
                    var SecUploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image", editList.ViewModelBoth.Article.AuthorId, newid);
                    Directory.Move(uploads, SecUploads);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }


    }


    
}