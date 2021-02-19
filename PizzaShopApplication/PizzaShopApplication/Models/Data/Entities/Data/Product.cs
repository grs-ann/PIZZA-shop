using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    /// <summary>
    /// Represents product entity.
    /// </summary>
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
        public bool Novelty { get; set; }
        public bool Bestseller { get; set; }
        public bool Discount { get; set; }
        // Link to ProductType table.
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        // Link to Image table.
        public Image Image { get; set; }
        public int ImageId { get; set; }
        // Link to ProductProperties table(navigation property).
        public virtual List<ProductProperty> ProductProperties { get; set; }
        public int ProductPropertyId { get; set; }
    }
}
