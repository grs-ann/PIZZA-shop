using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Context;

namespace PizzaShopApplication.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDataContext _dbContext;
        public ReviewController(ApplicationDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult GetAllComments()
        {
            return View();
        }
    }
}
