using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Authentification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PizzaShopApplication.Models.Secondary;
using Microsoft.AspNetCore.Identity;
using PizzaShopApplication.Models.AuthModels;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller manages accounts.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IPasswordHasher _hasher;
        private readonly UserManager<User> _userManager;
        public AccountController(ApplicationDataContext dbContext, IPasswordHasher hasher)
        {
            _dbContext = dbContext;
            _hasher = hasher;
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
                    .FirstOrDefaultAsync(u => u.Email == model.EmailOrLogin || u.Login == model.EmailOrLogin);
                if (user != null)
                {
                    if (!_hasher.IsPasswordMathcingHash(model.Password, user.Password))
                    {
                        ModelState.AddModelError("", "Неверный пароль!");
                    }
                    else
                    {
                        // Authentication.
                        await Authenticate(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Аккаунта с таким логином или email не существует!");
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
                // Checks if such a username or email already exists.
                if (_dbContext.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", $"Аккаунт с указанной почтой '{model.Email}' уже существует. Выберите другую.");
                    return View(model);
                }
                if (_dbContext.Users.Any(u => u.Login == model.Login))
                {
                    ModelState.AddModelError("", $"Аккаунт с указанным логином '{model.Login}' уже существует. Выберите другой.");
                    return View(model);
                }
                var hashPassword = _hasher.GenerateHash(model.Password);
                var user = new User
                {
                    Email = model.Email,
                    Password = hashPassword,
                    Name = model.Name,
                    Login = model.Login
                };
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
        [HttpGet]
        public IActionResult AccountSettings()
        {
            // User Id.
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultIssuer));
            var currentUser = _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId).Result;
            return View(currentUser);
        }
        [HttpPost]
        public async Task<IActionResult> AccountSettings(User user)
        {
            var userToChange = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            userToChange.Email = user.Email;
            userToChange.Name = user.Name;
            userToChange.Password = _hasher.GenerateHash(user.Password);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeName(User user)
        {
            var userToChange = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            userToChange.Name = user.Name;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("AccountSettings", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(User user)
        {
            var userToChange = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            userToChange.Email = user.Email;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("AccountSettings", "Account");
        }
        [HttpGet]
        public IActionResult ChangePassword(User user)
        {
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, User user)
        {
            var userToChange = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (_hasher.IsPasswordMathcingHash(oldPassword, userToChange.Password))
            {
                if (newPassword != null)
                {
                    userToChange.Password = _hasher.GenerateHash(newPassword);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("AccountSettings", "Account");
                }
                ModelState.AddModelError("", "Новый пароль не может быть пуст");
                return View(user);
            }
            ModelState.AddModelError("", "Неверно введен старый пароль");
            return View(user);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultIssuer, user.Id.ToString()),
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
