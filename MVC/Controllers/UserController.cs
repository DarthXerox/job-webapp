using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using DAL.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        readonly UserFacade userFacade;

        public UserController(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto user)
        {
            var userId = await userFacade.RegisterUserAsync(user);

            if (user.Role == Roles.JobSeeker)
            {
                return RedirectToAction("AddJobSeeker", "JobSeeker", new { userId });
            }
            return RedirectToAction("AddCompany", "Company", new { userId });
            /*try
            {
                //Here should be a check for existing user
                await userFacade.RegisterUserAsync(user);



                return RedirectToAction("Login", "User");


            }
            catch (Exception)
            {

                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View("Register");
            }*/
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLogin)
        {
            try
            {
                var user = await userFacade.LoginUserAsync(userLogin);

                await CreateClaimsAndSignInAsync(user);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Username", "Invalid credentials combination!");
                return View("Login");
            }
        }

        private async Task CreateClaimsAndSignInAsync(UserShowDto user)
        {
            var claims = new List<Claim>
            {
                //Set User Identity Name to actual user Id - easier access with user connected operations
                new Claim(ClaimTypes.Name, user.Id.ToString())
            };

            if (user.Role.HasFlag(Roles.Company))
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.Company.ToString()));
            }
            if (user.Role.HasFlag(Roles.JobSeeker))
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.JobSeeker.ToString()));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }


    }
}
