using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities;
using PizzaShopApplication.Models.Data.Entities.Authentification;
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
                        Discount = false,
                        //Id = 1
                    },
                    new Pizza
                    {
                        Name = "Куриная",
                        Diameter = 33,
                        Price = 300,
                        Novelty = true,
                        Bestseller = true,
                        Discount = false,
                        //Id = 2
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
                        IngridientId = 3
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        IngridientId = 4
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        IngridientId = 5
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        IngridientId = 6
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        IngridientId = 2
                    },
                    new Recept
                    {
                        PizzaId = 1,
                        IngridientId = 7
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        IngridientId = 7
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        IngridientId = 8
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        IngridientId = 9
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        IngridientId = 10
                    },
                    new Recept
                    {
                        PizzaId = 2,
                        IngridientId = 11
                    });
            }
            context.SaveChanges();
        }
    }
}
