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
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngridientId { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
