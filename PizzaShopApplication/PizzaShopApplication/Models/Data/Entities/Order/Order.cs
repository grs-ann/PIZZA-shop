using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopApplication.Models.Data.Entities.Order
{
    /// <summary>
    /// Represents user order entity.
    /// </summary>
    public class Order
    {
        // Order number.
        [Key]
        public int Id { get; set; }
        // Customer name.
        [Required(ErrorMessage = "Пожалуйста, укажите ваше имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя должно быть в пределах от 3 до 30 символов")]
        public string ClientName { get; set; }
        // Customer phone number.
        [Required(ErrorMessage = "Пожалуйста, укажите ваш номер телефона")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Некорректный номер телефона")]
        public string Phone { get; set; }
        // Customer email.
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Адрес почты должен быть в пределах от 7 до 50 символов")]
        public string Email { get; set; }
        // Street specified by the customer.
        [Required(ErrorMessage = "Пожалуйста, укажите улицу для доставки.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина назавания улицы от 1 до 50 символов!")]
        public string Street { get; set; }
        // House number specified by the customer.
        [Required(ErrorMessage = "Пожалуйста, укажите номер дома для доставки")]
        [StringLength(5, MinimumLength = 1, ErrorMessage ="Номер дома должен быть в пределах от 1 до 5 символов!")]
        public string Home { get; set; }
        // Apartment number specified by the customer 
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Номер квартиры должен быть в пределах от 1 до 5 символов!")]
        public string Apartment { get; set; }
        // Customer's floor number. 
        [Range(1, 3, ErrorMessage = "Максимальная длина для номера подъезда составляет 3 символа")]
        public int FloorNubmer { get; set; }
        // Customer's entrance number. 
        [Range(1, 3, ErrorMessage = "Максимальная длина для номера подъезда составляет 2 символа")]
        public int EntranceNubmer { get; set; }
        // Comments to the order. 
        [MaxLength(200, ErrorMessage = "Слишком длинный комментарий. Максимум - 200 символов")]
        public string Comment { get; set; }
        // Order datetime.
        public DateTime OrderDateTime { get; set; }

        // User cart guid (browser cookie).
        public Guid UserCartForeignKey { get; set; }

        // Link to the OrderStatus database table. 
        public OrderStatus OrderStatus { get; set; }
        public int OrderStatusId { get; set; }
    }
}
