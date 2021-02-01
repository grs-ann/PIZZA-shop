using PizzaShopApplication.Models.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

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
    }
} 
