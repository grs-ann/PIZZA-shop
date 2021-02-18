using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    public class Product
    {
        [ForeignKey("ProductProperty")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана стоимость")]
        [Range(0, 2000000, ErrorMessage = "Недопустимое значение стоимости")]
        public decimal Price { get; set; }
        // Является ли пицца новинкой.
        public bool Novelty { get; set; }
        // Является ли пицца хитом продаж.
        public bool Bestseller { get; set; }
        // Предоставлять ли на пиццу скидку.
        public bool Discount { get; set; }
        // Связь через FK с таблицей ProductType.
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        // Связь через FK с таблицей Image.
        public Image Image { get; set; }
        public int ImageId { get; set; }
        // Связь с таблицей ProductProperty(навигационное свойство).
        public virtual List<ProductProperty> ProductProperties { get; set; }
        public int ProductPropertyId { get; set; }
    }
}
