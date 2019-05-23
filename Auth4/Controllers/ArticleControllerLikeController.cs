using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightPathDev.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth4.Controllers
{
    public partial class ArticleController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like([Bind("ArticleId,ArticleTitle,desc_mini,desc,ArticleAdress,Articlecoor,ArticleContact,ImagePath,ImageName,AuthorId,AuthorName,FlagCount,Status")] Article article,int id)
        {
            return View();
        }
    }
}