using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.AuthModels;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PizzaShopApplication.Models.Secondary;

namespace PizzaShopApplication.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDataContext dbContext;
        private IPasswordHasher hasher;
        public AccountController(ApplicationDataContext dbContext, IPasswordHasher hasher)
        {
            this.dbContext = dbContext;
            this.hasher = hasher;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email);
                if (!hasher.IsPasswordMathcingHash(model.Password, user.Password))
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                else
                {
                    if (user != null)
                    {
                        // Аутентификация.
                        await Authenticate(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    var hashPassword = hasher.GenerateHash(model.Password);
                    user = new User { Email = model.Email, Password = hashPassword };
                    var userRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                    {
                        user.Role = userRole;
                    }
                    // Добавление пользователя в бд.
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                    // Аутентификация.
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректный логин и(или) пароль");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };  
            // Cоздаем объект ClaimsIdentity.
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
            // Установка аутентификационных кук.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
