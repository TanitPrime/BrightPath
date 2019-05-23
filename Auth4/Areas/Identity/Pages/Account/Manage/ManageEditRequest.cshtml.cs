using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BrightPathDev.Data;
using BrightPathDev.Models;
using BrightPathDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrightPathDev.Areas.Identity.Pages.Account
{

    public class EditRequest
    {
        public string UserName { get; set; }
        public string DateOfRequest { get; set; } 
        public int ArticleId { get; set; }
        public int OriginalId { get; set; }
        public ContactStatus Status { get; set; }

    }
    [Authorize(Roles ="Root,Admin")]
    public class ManageEditRequest : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public ManageEditRequest(
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

        

        public IList<EditList> editRequests = new List<EditList>();
       
        public ActionResult  OnGetAsync(EditList editList)
        {
            

            //var editrq = _context.Articles.ToList();
            foreach(var u in editRequests)
            {
                EditList X = new EditList
                {
                    ViewModelBoth = editList.ViewModelBoth,
                    
                    OriginalId = editList.OriginalId                     
                };
                 editRequests.Add(X);
            }
            
            return Page();


        }




    }
}
