using PizzaShopApplication.Models.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PizzaShopApplication.Models.Data.Domain
{
    public class PizzaRepository
    {
        private readonly ApplicationDataContext dbContext;
        public PizzaRepository(ApplicationDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // Получает список всех видов пицц, доступных в базе данных.
        public async Task<IEnumerable<Pizza>> GetPizzasAsync()
        {
            var result = await dbContext.Pizzas.ToListAsync();
            return result;
        }
        // Получает пиццу, находящуюся в БД по id.
        public async Task<Pizza> GetPizzaAsync(int id)
        {
            var pizza = await dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id);
            return pizza;
        }
        public async Task SaveChangesAsync(Pizza pizza, int id)
        {
            var pizzaToChange = dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id).Result;
            pizzaToChange.Name = pizza.Name;
            pizzaToChange.Price = pizza.Price;
            pizzaToChange.Diameter = pizza.Diameter;
            pizzaToChange.Novelty = pizza.Novelty;
            pizzaToChange.Bestseller = pizza.Bestseller;
            pizzaToChange.Discount = pizza.Discount;
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var pizzaToDelete = await dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id);
            dbContext.Pizzas.Remove(pizzaToDelete);
            await dbContext.SaveChangesAsync();
        }
        public async Task AddNewPizzaAsync(Pizza pizza)
        {
            var newPizza = new Pizza()
            {
                Name = pizza.Name,
                Price = pizza.Price,
                Diameter = pizza.Diameter,
                Novelty = pizza.Novelty,
                Bestseller = pizza.Bestseller,
                Discount = pizza.Discount
            };
            dbContext.Pizzas.Add(newPizza);
            await dbContext.SaveChangesAsync();
        }
    }
} 
