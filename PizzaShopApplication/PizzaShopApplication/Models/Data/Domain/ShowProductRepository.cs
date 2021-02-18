using PizzaShopApplication.Models.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Data.Domain.Interfaces;
using System.Linq;

namespace PizzaShopApplication.Models.Data.Domain
{
    /// <summary>
    /// This class represents easy way to get products 
    /// from database.
    /// </summary>
    public class ShowProductRepository : IProduct
    {

        private readonly ApplicationDataContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;
        public ShowProductRepository(ApplicationDataContext dbContext, IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
        }
        /// <summary>
        /// Getting a product contained in database by his Id.
        /// </summary>
        /// <param name="id">Unique product identificator in DB table</param>
        /// <returns>Product</returns>
        public async Task<Product> GetProductFromDBAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
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
