using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data
{
    // Данный класс предоставляет таблицу для всех ингредиентов в БД
    // с полями, соответствующими определенным свойствам.
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        // Название ингридиента
        public string Name { get; set; }
    }
}
