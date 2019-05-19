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

    public class Request
    {
        public string UserName { get; set; }
        public string DateOfRequest { get; set; }

        public string reason { get; set; }

        public int ArticleId { get; set; }

        public int DListId { get; set; }
    }
    [Authorize(Roles ="Root,Admin")]
    public class ManageDeleteRequestModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public ManageDeleteRequestModel(
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



        public IList<Request> Requests = new List<Request>();

        public async Task OnGetAsync()
        {
            var requests = _context.DeleteLists.ToList();
            foreach(var u in requests)
            {
                Request X = new Request
                {
                    UserName = u.AuthorName,
                    DateOfRequest = u.DateOfRequest,
                    reason = u.Reason,
                    ArticleId = u.ArticleId,
                    DListId = u.DListId
                };
                Requests.Add(X);
            }



        }




    }
}
