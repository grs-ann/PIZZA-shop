using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        // Ссылка на связанную модель Pizza.
        public int PizzaId { get; set; }
        // Ссылка на связанную модель Recept.
        public int ReceptId { get; set; }
    }
}
