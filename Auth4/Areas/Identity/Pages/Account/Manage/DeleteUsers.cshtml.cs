using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BrightPathDev.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BrightPathDev.Areas.Identity.Pages.Account
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    [Authorize(Roles ="Root")]
    public class DeleteUserModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public DeleteUserModel(
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
     

        public IList<User> Users = new List<User>();



        public void OnGet()
        {
            List<IdentityUser> AppUsers = _context.Users.ToList();
            foreach (IdentityUser U in AppUsers)
            {
                User X = new User
                {
                    UserId = U.Id,
                    UserName = U.NormalizedUserName
                };
                Users.Add(X);
            }
        }

      


    }
}
