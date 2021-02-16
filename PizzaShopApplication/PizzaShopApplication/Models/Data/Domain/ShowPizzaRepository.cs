using PizzaShopApplication.Models.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary.Entities;
using PizzaShopApplication.Models.Secondary;
using PizzaShopApplication.Models.Data.Domain.Abstract;
using PizzaShopApplication.Models.Data.Domain.Interfaces;

namespace PizzaShopApplication.Models.Data.Domain
{
    public class ShowPizzaRepository : ProductRepository<Pizza>
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;
        public ShowPizzaRepository(ApplicationDataContext dbContext, IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
        }
        // Получает пиццу, находящуюся в БД по id.
        public override async Task<Pizza> GetProductFromDBAsync(int id)
        {
            var pizza = await _dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id);
            return pizza;
        }
        // Получает список всех видов пицц, доступных в базе данных.
        public override IEnumerable<Pizza> GetProductsFromDB()
        {
            var pizzas = _dbContext.Pizzas.Include(p => p.Image);
            return pizzas;
        }
    }
} 
