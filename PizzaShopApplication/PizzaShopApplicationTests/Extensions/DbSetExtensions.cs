using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShopApplicationTests.Extensions
{
    /// <summary>
    /// Extension to Db Set. Clears db set completly.
    /// </summary>
    internal static class DbSetExtensions
    {
        /// <summary>
        /// Clears db set completly.
        /// </summary>
        /// <typeparam name="T">Type of entities in db set.</typeparam>
        /// <param name="dbSet">Db set to be cleared.</param>
        /// <returns></returns>
        public static async Task ClearIfAny<T>(this DbSet<T> dbSet) where T : class
        {
            if (await dbSet.AnyAsync())
            {
                dbSet.RemoveRange(dbSet);
            }
        }
    }
}
