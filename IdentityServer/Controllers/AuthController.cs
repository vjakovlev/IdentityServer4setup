using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl) 
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel) 
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(loginModel.ReturnUrl);
            }
            else if (result.IsLockedOut) 
            {
            
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(registerModel);
            }

            var user = new IdentityUser(registerModel.Username);
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded) 
            {
                await _signInManager.SignInAsync(user, false); 
                return Redirect(registerModel.ReturnUrl);
            }


            return View();
        }
    }
}
