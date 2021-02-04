<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
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
            var pizza1 = Guid.NewGuid();
            var pizza2 = Guid.NewGuid();
            var ingredient2 = Guid.NewGuid();
            var ingredient3 = Guid.NewGuid();
            var ingredient4 = Guid.NewGuid();
            var ingredient5 = Guid.NewGuid();
            var ingredient6 = Guid.NewGuid();
            var ingredient7 = Guid.NewGuid();
            var ingredient8 = Guid.NewGuid();
            var ingredient9 = Guid.NewGuid();
            var ingredient10 = Guid.NewGuid();
            var ingredient11 = Guid.NewGuid();
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
                        Id = pizza1
                    },
                    new Pizza
                    {
                        Name = "Куриная",
                        Diameter = 33,
                        Price = 300,
                        Novelty = true,
                        Bestseller = true,
                        Discount = false,
                        Id = pizza2
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
                        PizzaId = pizza1,
                        IngridientId = ingredient3
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient4
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient5
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient6
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient2
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient7
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient7
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient8
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient9
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient10
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient11
                    });
            }
            context.SaveChanges();
        }
    }
}
=======
﻿using PizzaShopApplication.Models.Data.Context;
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
            var pizza1 = Guid.NewGuid();
            var pizza2 = Guid.NewGuid();
            var ingredient2 = Guid.NewGuid();
            var ingredient3 = Guid.NewGuid();
            var ingredient4 = Guid.NewGuid();
            var ingredient5 = Guid.NewGuid();
            var ingredient6 = Guid.NewGuid();
            var ingredient7 = Guid.NewGuid();
            var ingredient8 = Guid.NewGuid();
            var ingredient9 = Guid.NewGuid();
            var ingredient10 = Guid.NewGuid();
            var ingredient11 = Guid.NewGuid();
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
                        Id = pizza1
                    },
                    new Pizza
                    {
                        Name = "Куриная",
                        Diameter = 33,
                        Price = 300,
                        Novelty = true,
                        Bestseller = true,
                        Discount = false,
                        Id = pizza2
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
                        PizzaId = pizza1,
                        IngridientId = ingredient3
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient4
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient5
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient6
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient2
                    },
                    new Recept
                    {
                        PizzaId = pizza1,
                        IngridientId = ingredient7
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient7
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient8
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient9
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient10
                    },
                    new Recept
                    {
                        PizzaId = pizza2,
                        IngridientId = ingredient11
                    });
            }
            context.SaveChanges();
        }
    }
}
>>>>>>> parent of 8503132... Merge pull request #7 from grs-ann/AuthentificationSystem
