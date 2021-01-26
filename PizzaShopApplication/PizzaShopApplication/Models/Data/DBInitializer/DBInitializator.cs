using PizzaShopApplication.Models.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.DBInitializer
{
    public static class DBInitializator
    {
        // При пустой БД заполняет таблицы тестовыми начальными данными.
        public static void Initialize(ApplicationDataContext context)
        {
            if (!context.Pizzas.Any())
            {
                context.Pizzas.AddRange(
                    new Pizza
                    {
                        Name = "4 сыра",
                        Diameter = 33,
                        Price = 289,
                        Novelty = true,
                        Bestseller = true,
                        Discount = false
                    },
                    new Pizza
                    {
                        Name = "Куриная",
                        Diameter = 33,
                        Price = 300,
                        Novelty = true,
                        Bestseller = true,
                        Discount = false
                    });
            }
            if (!context.Ingredients.Any())
            {
                context.Ingredients.AddRange(
                    new Ingredient
                    {
                        Name = "Помидоры"
                    },
                    new Ingredient
                    {
                        Name = "Огурцы"
                    },
                    new Ingredient
                    {
                        Name = "Сыр пармезан"
                    },
                    new Ingredient
                    {
                        Name = "Сыр чеддер"
                    },
                    new Ingredient
                    {
                        Name = "Сыр сулугуни"
                    },
                    new Ingredient
                    {
                        Name = "Сыр брынза"
                    },
                    new Ingredient
                    {
                        Name = "Тонкое тесто"
                    },
                    new Ingredient
                    {
                        Name = "Сыр моцарелла"
                    },
                    new Ingredient
                    {
                        Name = "Лук"
                    },
                    new Ingredient
                    {
                        Name = "Курица"
                    },
                    new Ingredient
                    {
                        Name = "Томаты"
                    });
            }
            if (!context.Recepts.Any())
            {
                context.Recepts.AddRange(
                    new Recept
                    {
                        PizzaId = 1,
                        ReceptId = 3
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        ReceptId = 4
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        ReceptId = 5
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        ReceptId = 6
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        ReceptId = 2
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        ReceptId = 7
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        ReceptId = 7
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        ReceptId = 8
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        ReceptId = 9
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        ReceptId = 10
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        ReceptId = 11
                    });
            }
            context.SaveChanges();
        }
    }
}
