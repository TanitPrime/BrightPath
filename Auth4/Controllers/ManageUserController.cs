using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightPathDev.Areas.Identity.Pages.Account;
using BrightPathDev.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auth4.Controllers
{
    public class ManageUserController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ManageUserController(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        //delete user
        [Authorize(Roles = "Root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var logins = await _userManager.GetLoginsAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);

            using (var transaction = _context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                foreach (var login in logins)
                {
                    result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit(); //only commit if user and all his logins/roles have been deleted  
                }
            }
           return LocalRedirect("/Identity/Account/Manage/ManageUsers");

        }



        //add silenced role to user
        [Authorize(Roles = "Root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Mute(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            


            if (ModelState.IsValid)
            {



                var user = await _userManager.FindByIdAsync(id);
                var logins = await _userManager.GetLoginsAsync(user);
                var rolesForUser = await _userManager.GetRolesAsync(user);


                using (var transaction = _context.Database.BeginTransaction())
                {
                    IdentityResult result = IdentityResult.Success;

                foreach (var login in logins)
                {
                    result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.AddToRoleAsync(user, "Muted");
                    if (result == IdentityResult.Success)
                        transaction.Commit(); //only commit if user and all his logins/roles have been deleted  
                }


                }



            }

          
            return LocalRedirect("/Identity/Account/Manage/ManageUsers");

        }



    }
}


