using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data
{
    // Данный класс предоставляет таблицу в БД
    // которая является связующей между таблицами Pizza и Recept.
    public class Recept
    {
        [Key]
        public Guid Id { get; set; }
        // Ссылка на связанную модель Pizza.
        [ForeignKey("Pizza")]
        public Guid PizzaId { get; set; }
        // Ссылка на связанную модель Recept.
        [ForeignKey("Ingredient")]
        public Guid IngridientId { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
