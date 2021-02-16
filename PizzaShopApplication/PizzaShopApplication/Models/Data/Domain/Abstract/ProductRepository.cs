using PizzaShopApplication.Models.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain.Abstract
{
    public abstract class ProductRepository<T> where T: IProduct
    {
        // Получает продукт из базы данных по Id.
        public abstract Task<T> GetProductFromDBAsync(int id);
        // Получает перечисление всех продуктов из базы данных.
        public abstract IEnumerable<T> GetProductsFromDB();
    }
}
