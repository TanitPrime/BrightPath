using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BrightPathDev.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrightPathDev.Areas.Identity.Pages.Account
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string UserRole { get; set; }
    }

    [Authorize(Roles ="Root")]
    public class ManageUserModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public ManageUserModel(
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


        
        public async Task OnGetAsync()
        {
            List<IdentityUser> AppUsers = _context.Users.ToList();
            foreach (IdentityUser U in AppUsers)
            {
               
                User X = new User
                {
                    UserId = U.Id,
                    UserName = U.NormalizedUserName,
                    
                };
                var thisuser = await _userManager.FindByIdAsync(X.UserId);
                var IdentityRolelist =await _userManager.GetRolesAsync(thisuser);
                
                IdentityUser au = _context.Users.First(u => u.Id == X.UserId);

                
                //AspNetUser selectedUser = dbContext.AspNetUsers.FirstOrDefault(u => u.UserId == X.UserId);
                Users.Add(X);
            }
        }

        
        

    }
}
