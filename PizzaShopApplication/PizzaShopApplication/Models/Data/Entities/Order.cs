using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data
{
    // Данный класс предоставляет таблицу для заказов в БД
    // с полями, соответствующими определенным свойствам.
    public class Order
    {
        [Key]
        public int Id { get; set; }
        // Имя заказчика.
        [Required]
        [MaxLength(30)]
        public int Name { get; set; }
        // Номер телефона заказчика.
        [Required]
        public int Phone { get; set; }
        // Почта заказчика.
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        // Улица, указанная заказчиком.
        [Required]
        [MaxLength(30)]
        public string Street { get; set; }
        // Номер дома, указанный заказчиком.
        [Required]
        public string Home { get; set; }
        // Квартира, указанная заказчиком
        public int Apartment { get; set; }
        // Комментарий к заказу.
        public string Comment { get; set; }
    }
}
