using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGallery.Areas.Admin.Models;
using VideoGallery.Areas.Admin.Models.ViewModels;
using VideoGallery.Models;

namespace VideoGallery.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityCustomeUser> userManager;
        private SignInManager<IdentityCustomeUser> signinManager;
        private readonly ApplicationDbContext context;
        public AccountController(UserManager<IdentityCustomeUser> _userManager, SignInManager<IdentityCustomeUser> _signinManager, ApplicationDbContext _context)
        {
            userManager = _userManager;
            signinManager = _signinManager;
            context = _context;
        }
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

           if (ModelState.IsValid)
            {
                var user = context.Users.SingleOrDefault(e => e.UserName == model.UserEmail);
                if (user != null)
                {
                    // var result=await signinManager.CheckPasswordSignInAsync(user, model.Password, false);
                    var result = await signinManager.PasswordSignInAsync(model.UserEmail, model.Password, false, false);
                    if (result.Succeeded)  
                    {
                        return RedirectToAction("Index","Home",new { @Areas="Admin"});
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login Credentials!");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "invalid user!");
                    return View();
                }
               
            }
            else
            {
                return View(model);
            }
            
        }
        public IActionResult CreateUser()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityCustomeUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    PasswordHash = model.Password,
                    PhoneNumber = model.Phone,
                    Gender = model.Gender,
                      

                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.message = "User Create Successfully !";
                    return View();
                }
                else
                {
                    foreach (var er in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, er.Description);
                    }
                    return View();
                }
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Delete()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("Login");
        }
}   }

