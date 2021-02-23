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
using PizzaShopApplication.Models.Data.Domain;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller manages accounts.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IPasswordHasher _hasher;
        private readonly EditAccountRepository _editAccountRepository;
        public AccountController(ApplicationDataContext dbContext, IPasswordHasher hasher,
            EditAccountRepository editAccountRepository)
        {
            _dbContext = dbContext;
            _hasher = hasher;
            _editAccountRepository = editAccountRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Allows the user to log into their account. 
        /// </summary>
        /// <param name="model">Login model for data validation.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    if (!_hasher.IsPasswordMathcingHash(model.Password, user.Password))
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                    else
                    {
                        // Authentication.
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
        /// <summary>
        /// Allows the user to register new account. 
        /// </summary>
        /// <param name="model">Register model for data validation.</param>
        /// <returns></returns>
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    var hashPassword = _hasher.GenerateHash(model.Password);
                    user = new User { Email = model.Email, Password = hashPassword };
                    var userRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                    {
                        user.Role = userRole;
                    }
                    // Adding new user account to database "Users" table.
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    // Authentication.
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
        /// <summary>
        /// Allows the user to logout from account. 
        /// </summary>
        /// <returns></returns>
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
            // Create a new ClaimsIdentity object.
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
            // Setting authenticate cookies.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
