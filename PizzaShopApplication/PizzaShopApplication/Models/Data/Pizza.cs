using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data
{
    // Данный класс предоставляет таблицу для пицц в БД
    // с полями, соответствующими определенным свойствам.
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        // Название пиццы.
        public string Name { get; set; }
        // Диаметр пиццы в сантиметрах.
        public int Diameter { get; set; }
        // Стоимость пиццы.
        public decimal Price { get; set; }
        // Является ли пицца новинкой.
        public bool Novelty { get; set; }
        // Является ли пицца хитом продаж.
        public bool Bestseller { get; set; }
        // Предоставлять ли на пиццу скидку.
        public bool Discount { get; set; }        
    }
}
