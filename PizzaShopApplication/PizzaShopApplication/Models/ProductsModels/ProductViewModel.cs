using PizzaShopApplication.Models.Data.Entities.Products;
using System.Collections.Generic;

namespace PizzaShopApplication.Models.ProductModels
{
    /// <summary>
    /// The purpose of this class is to pass data to the View.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pizzas">Pizzas collection</param>
        /// <param name="drinks">Drinks collection</param>
        public ProductViewModel(IEnumerable<Product> pizzas, IEnumerable<Product> drinks)
        {
            Pizzas = pizzas;
            Drinks = drinks;
        }
        public IEnumerable<Product> Pizzas { get; set; }
        public IEnumerable<Product> Drinks { get; set; }
    } 
}
