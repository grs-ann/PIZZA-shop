using PizzaShopApplication.Models.Data.Entities.Data;
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
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }
        // Диаметр пиццы в сантиметрах.
        [Required(ErrorMessage = "Не указан диаметр")]
        [Range(15, 100, ErrorMessage = "Недопустимое значение диаметра")]
        public int Diameter { get; set; }
        // Стоимость пиццы.
        [Required(ErrorMessage = "Не указана стоимость")]
        [Range(0, 2000000, ErrorMessage = "Недопустимое значение стоимости")]
        public decimal Price { get; set; }
        // Является ли пицца новинкой.
        public bool Novelty { get; set; }
        // Является ли пицца хитом продаж.
        public bool Bestseller { get; set; }
        // Предоставлять ли на пиццу скидку.
        public bool Discount { get; set; }
        // Id изображения в таблице Images.
        public int ImageId { get; set; }
        // Связь таблицы Pizzas и таблицы Images.
        public Image Image { get; set; }
    }
}
