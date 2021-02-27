using PizzaShopApplication.Models.Data.Context;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using PizzaShopApplication.Models.Domain.Interfaces;
using System.Linq;
using PizzaShopApplication.Models.ProductModels;
using PizzaShopApplication.Models.Data.Entities.Products;

namespace PizzaShopApplication.Models.Domain
{
    /// <summary>
    /// This class represents easy way to get products 
    /// from database.
    /// </summary>
    public class ShowProductRepository : IProduct
    {

        private readonly ApplicationDataContext _dbContext;
        public ShowProductRepository(ApplicationDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Getting a product contained in database by his Id.
        /// </summary>
        /// <param name="id">Unique product identificator in DB table</param>
        /// <returns>Product</returns>
        public Product GetProductFromDB(int id)
        {
            var product = _dbContext.Products.Include(p => p.ProductProperties).FirstOrDefaultAsync(p => p.Id == id).Result;
            return product;
        }
        /// <summary>
        /// Gets a product with pizza type, and fills
        /// the PizzaViewModel with this data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PizzaViewModel</returns>
        public PizzaViewModel GetPizzaViewModel(int id)
        {
            var product = _dbContext.Products.Include(p => p.ProductProperties).FirstOrDefaultAsync(p => p.Id == id).Result;
            var pizzaViewModel = new PizzaViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Bestseller = product.Bestseller,
                Discount = product.Discount,
                Novelty = product.Novelty,
                PizzaIngridients = product.ProductProperties.FirstOrDefault(p => p.Id == product.Id).Value
            };
            return pizzaViewModel;
        }
        /// <summary>
        /// Gets a product with drink type, and fills
        /// the DrinkViewModel with this data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DrinkViewModel</returns>
        public DrinkViewModel GetDrinkViewModel(int id)
        {
            var product = _dbContext.Products.Include(p => p.ProductProperties).FirstOrDefaultAsync(p => p.Id == id).Result;
            var drinkViewModel = new DrinkViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Bestseller = product.Bestseller,
                Discount = product.Discount,
                Novelty = product.Novelty
            };
            return drinkViewModel;
        }
        /// <summary>
        /// Getting a enumerable of all products
        /// contained in database
        /// </summary>
        /// <returns>IEnumerable<Product></returns>
        public IEnumerable<Product> GetAllProductsFromDB()
        {
            //var products = _dbContext.Products.Include(p => p.Image).Include(p => p.ProductProperty);
            var products = _dbContext.Products.Include(p => p.Image);
            //return products;
            return products;
        }
        /// <summary>
        /// Getting a Enumerable of all pizzas
        /// contained in database
        /// </summary>
        /// <returns>IEnumerable<Product></returns>
        public IEnumerable<Product> GetAllPizzasFromDB()
        {
            var pizzas = _dbContext.Products.Include(p => p.Image)
                .Include(p => p.ProductProperties)
                .Where(p => p.ProductType.Id == 1);
            return pizzas;
        }
        /// <summary>
        /// Getting a Enumerable of all drinks
        /// contained in database
        /// </summary>
        /// <returns>IEnumerable<Product></returns>
        public IEnumerable<Product> GetAllDrinksFromDB()
        {
            var drinks = _dbContext.Products.Include(d => d.Image)
                .Include(d => d.ProductProperties)
                .Where(d => d.ProductType.Id == 2);
            return drinks;
        }
    }
} 
