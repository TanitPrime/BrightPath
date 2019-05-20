using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BrightPathDev.Data;
using BrightPathDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrightPathDev.Areas.Identity.Pages.Account
{

    public class Rq
    {
        public string UserName { get; set; }
        public string DateOfRequest { get; set; } 
        public int ArticleId { get; set; }

        public ContactStatus Status { get; set; }

    }
    [Authorize(Roles ="Root,Admin")]
    public class Approvals : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public Approvals(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;

        }



        public IList<Rq> rqs = new List<Rq>();

        public async Task OnGetAsync()
        {
            var rq = _context.Articles.ToList();
            foreach(var u in rq)
            {
                Rq X = new Rq
                {
                    UserName = u.AuthorName,
                    DateOfRequest = DateTime.Now.ToString().ToString(),
                    ArticleId = u.ArticleId,
                    Status = u.Status
                };
                 rqs.Add(X);
            }



        }




    }
}
