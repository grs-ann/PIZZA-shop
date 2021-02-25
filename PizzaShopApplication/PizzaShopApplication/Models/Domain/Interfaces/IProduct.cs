using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.ProductModels;
using System.Collections.Generic;

namespace PizzaShopApplication.Models.Domain.Interfaces
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
