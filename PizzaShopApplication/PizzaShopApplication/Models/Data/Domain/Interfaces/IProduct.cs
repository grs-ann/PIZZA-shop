using PizzaShopApplication.Models.Data.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain.Interfaces
{
    /// <summary>
    /// This interface defines base methods, which
    /// that should be in his class-inheritor for
    /// getting a products from database.
    /// </summary>
    interface IProduct
    {
        // Получает продукт из базы данных по Id.
        public Task<Product> GetProductFromDBAsync(int id);
        // Получает перечисление всех продуктов из базы данных.
        public IEnumerable<Product> GetAllProductsFromDB();
        public IEnumerable<Product> GetAllPizzasFromDB();
        public IEnumerable<Product> GetAllDrinksFromDB();
    }
}
