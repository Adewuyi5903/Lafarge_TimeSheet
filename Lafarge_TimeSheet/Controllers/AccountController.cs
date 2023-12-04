using Lafarge_TimeSheet.Context;
using Lafarge_TimeSheet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lafarge_TimeSheet.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Register(Register register)
		{
			if (ModelState.IsValid)
			{
                var user = await _userManager.FindByEmailAsync(register.EmailAdress);
                if (user != null)
                {
                    TempData["Error"] = "This email adress is already in use!";
                    return View(register);
                }

                var newUser = new ApplicationUser()
                {
                    FirstName = register.FirstName,
					LastName = register.LastName,
                    Email = register.EmailAdress,
                    UserName = register.EmailAdress,
					EmailConfirmed = true
                }; 
                var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);

                if (newUserResponse.Succeeded) 
				{
                    await _userManager.AddToRoleAsync(newUser, "User");
                }

                TempData["Success"] = "Account registered successfully";
                return RedirectToAction("Login");
            }
			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(Login login)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(login.Email);
				if (user != null)
				{
					var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
					if (passwordCheck)
					{
						var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						TempData["Error"] = "Wrong credentials. Please, try again!";
						return View(login);
					}
				}
				TempData["Error"] = "Wrong credentials. Please, try again!";
				return View(login);
			}
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
