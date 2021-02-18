using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities;
using PizzaShopApplication.Models.Data.Entities.Authentification;
using PizzaShopApplication.Models.Data.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.DBInitializer
{
    public static class DBInitializator
    {
        private static Guid[] randomGuids = new Guid[20];
        // При пустой БД заполняет таблицы тестовыми начальными данными.
        public static void Initialize(ApplicationDataContext context)
        {
            if (!context.Images.Any())
            {
                context.Images.AddRange(
                    new Image
                    {
                        Name = "cheese.png",
                        Path = "/images/"
                    },
                    new Image
                    {
                        Name = "chicken.png",
                        Path = "/images/"
                    },
                    new Image
                    {
                        Name = "bonaqua.png",
                        Path = "/images/"
                    });
                context.SaveChanges();
            }
            if (!context.ProductTypes.Any())
            {
                context.ProductTypes.AddRange(
                    new ProductType
                    {
                        Name = "Пицца"
                    },
                    new ProductType
                    {
                        Name = "Напиток"
                    });
                context.SaveChanges();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "4 сыра",
                        Price = 389,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 1,
                        ProductTypeId = 1,
                        ProductPropertyId = 1
                    },
                    new Product
                    {
                        Name = "Куриная",
                        Price = 349,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 2,
                        ProductTypeId = 1,
                        ProductPropertyId = 1
                    },
                    new Product
                    {
                        Name = "BonAqua",
                        Price = 70,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 3,
                        ProductTypeId = 2
                    });
                context.SaveChanges();
            }
            if (!context.Properties.Any())
            {
                context.Properties.AddRange(
                    new Property
                    {
                        ProductTypeId = 1,
                        Name = "Ингредиенты",
                    });
                context.SaveChanges();
            }
            if (!context.ProductProperties.Any())
            {
                context.ProductProperties.AddRange(
                    new ProductProperty
                    {
                        Value = "белый соус, курица, лук, моцарелла, орегано, томаты",
                        ProductId = 2,
                        PropertyId = 1
                    },
                    new ProductProperty
                    {
                        Value = "базилик, дорблю, моцарелла, пармезан, сливочный сыр, сырный соус",
                        ProductId = 3,
                        PropertyId = 1
                    });
            }
            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses.AddRange(
                    // Заказ в процессе доставки.
                    new OrderStatus
                    {
                        InProcess = true,
                        Cancelled = false,
                        Status = "В процессе доставки"
                    },
                    // Заказ доставлен.
                    new OrderStatus
                    {
                        InProcess = false,
                        Cancelled = false,
                        Status = "Заказ доставлен"
                    },
                    // Заказ отменён.
                    new OrderStatus
                    {
                        InProcess = false,
                        Cancelled = true,
                        Status = "Заказ отменён"
                    });
            }
            context.SaveChanges();
            if (!context.Carts.Any())
            {
                Random random = new Random();
                for (int i = 0; i < 20; i++)
                {
                    var cart = new Cart
                    {
                        UserId = Guid.NewGuid(),
                        Quantity = random.Next(1, 3),
                        DateCreated = DateTime.UtcNow,
                        Product = context.Products.FirstOrDefault(),
                    };
                    randomGuids[i] = cart.UserId;
                    context.Carts.Add(cart);
                }
            }
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order
                    {
                        ClientName = "Паша",
                        Phone = "8927748-81-83",
                        Email = "theBestJuniorInTheWorld@gmail.com",
                        Street = "Промышленности",
                        Home = "228-A",
                        Apartment = "58",
                        EntranceNubmer = 4,
                        FloorNubmer = 4,
                        Comment = "Побыстрее, пожалуйста!",
                        OrderDateTime = DateTime.UtcNow,
                        UserCartForeignKey = randomGuids[0],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 3)
                    },
                    new Order
                    {
                        ClientName = "Максим",
                        Phone = "8-927-322-14-88",
                        Email = "42ama@gmail.com",
                        Street = "Можайский пер.",
                        Home = "100",
                        Apartment = "10",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        Comment = "хочется кушать",
                        OrderDateTime = DateTime.UtcNow,
                        UserCartForeignKey = randomGuids[1],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 3)
                    },
                    new Order
                    {
                        ClientName = "Макс",
                        Phone = "843-13-59",
                        Email = "domolim@mail.ru",
                        Street = "Промышленности",
                        Home = "319",
                        Apartment = "49",
                        EntranceNubmer = 3,
                        FloorNubmer = 4,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[2],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Вика",
                        Phone = "207-52-52",
                        Street = "Елизарова",
                        Home = "16",
                        Apartment = "5",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[3],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Вика",
                        Phone = "207-52-52",
                        Street = "Елизарова",
                        Home = "16",
                        Apartment = "5",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[4],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Вика",
                        Phone = "207-52-52",
                        Street = "Елизарова",
                        Home = "16",
                        Apartment = "5",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[5],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Айбол",
                        Phone = "518-85-20",
                        Street = "Гагарина",
                        Home = "120",
                        Apartment = "66",
                        EntranceNubmer = 1,
                        FloorNubmer = 6,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[6],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Саша",
                        Phone = "575-47-06",
                        Street = "Полевая",
                        Home = "33",
                        Apartment = "14",
                        EntranceNubmer = 1,
                        FloorNubmer = 2,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[7],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Анна",
                        Phone = "340-14-22",
                        Street = "Посёлок старосемейкино",
                        Home = "1",
                        Apartment = "1",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[8],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 1),
                        Comment = "я умная"
                    },
                    new Order
                    {
                        ClientName = "Дарья",
                        Phone = "918-70-51",
                        Street = "Агибалова",
                        Home = "5",
                        Apartment = "70",
                        EntranceNubmer = 1,
                        FloorNubmer = 2,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[9],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Машечка",
                        Phone = "207-52-52",
                        Street = "Агибалова",
                        Home = "5",
                        Apartment = "108",
                        EntranceNubmer = 6,
                        FloorNubmer = 7,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[10],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Дмитрий",
                        Phone = "590-56-36",
                        Street = "Льва Толстого",
                        Home = "148Б",
                        Apartment = "55",
                        EntranceNubmer = 3,
                        FloorNubmer = 5,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[11],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Дмитрий",
                        Phone = "590-56-36",
                        Street = "Льва Толстого",
                        Home = "148Б",
                        Apartment = "55",
                        EntranceNubmer = 3,
                        FloorNubmer = 5,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[12],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Олег",
                        Phone = "709-75-80",
                        Street = "Вилоновская",
                        Home = "144",
                        Apartment = "55",
                        EntranceNubmer = 3,
                        FloorNubmer = 5,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[13],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Олег",
                        Phone = "709-75-80",
                        Street = "Вилоновская",
                        Home = "144",
                        Apartment = "55",
                        EntranceNubmer = 3,
                        FloorNubmer = 5,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[14],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Олег",
                        Phone = "709-75-80",
                        Street = "Вилоновская",
                        Home = "144",
                        Apartment = "55",
                        EntranceNubmer = 3,
                        FloorNubmer = 5,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[15],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Ольга",
                        Email = "olga@mail.ru",
                        Phone = "709-75-80",
                        Street = "Вилоновская",
                        Home = "77",
                        Apartment = "22",
                        EntranceNubmer = 3,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[16],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Василиса",
                        Email = "vasilisa1997@mail.ru",
                        Phone = "132-92-96",
                        Street = "Чапаевская",
                        Home = "13",
                        Apartment = "13",
                        EntranceNubmer = 1,
                        FloorNubmer = 3,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[17],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2),
                        Comment = "Позвонить по телефону, домофон не работает"
                    },
                    new Order
                    {
                        ClientName = "Алёна",
                        Phone = "987-09-37",
                        Street = "Некрасовская",
                        Home = "66",
                        Apartment = "4",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[18],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    },
                    new Order
                    {
                        ClientName = "Алёна",
                        Phone = "987-09-37",
                        Street = "Некрасовская",
                        Home = "66",
                        Apartment = "4",
                        EntranceNubmer = 1,
                        FloorNubmer = 1,
                        OrderDateTime = GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.UtcNow),
                        UserCartForeignKey = randomGuids[19],
                        OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Id == 2)
                    });
            }

            context.SaveChanges();
        }
        // Генерариует случайную дату в указанном временном диапазоне.
        public static DateTime GetRandomDateTime(DateTime from, DateTime to)
        {
            if (from >= to)
            {
                throw new Exception("Параметр \"from\" должен быть меньше параметра \"to\"!");
            }
            var random = new Random();
            TimeSpan range = to - from;
            var randts = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            return from + randts;
        }
    }
}
