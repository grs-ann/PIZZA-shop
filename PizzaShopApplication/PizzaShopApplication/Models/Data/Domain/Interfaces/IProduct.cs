using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain.Interfaces
{
    /// <summary>
    /// This interface defines base methods, which
    /// that should be in class-inheritor for
    /// getting a products from database.
    /// </summary>
    public interface IProduct
    {
        public Product GetProductFromDB(int id);
        public IEnumerable<Product> GetAllProductsFromDB();
        public IEnumerable<Product> GetAllPizzasFromDB();
        public IEnumerable<Product> GetAllDrinksFromDB();
        public PizzaViewModel GetPizzaViewModel(int id);
        public DrinkViewModel GetDrinkViewModel(int id);
    }
}
