using PizzaShopApplication.Models.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary.Entities;
using PizzaShopApplication.Models.Secondary;

namespace PizzaShopApplication.Models.Data.Domain
{
    public class PizzaRepository
    {
        private readonly ApplicationDataContext dbContext;
        private readonly IWebHostEnvironment appEnvironment;
        public PizzaRepository(ApplicationDataContext dbContext, IWebHostEnvironment appEnvironment)
        {
            this.dbContext = dbContext;
            this.appEnvironment = appEnvironment;
        }
        // Получает список всех видов пицц, доступных в базе данных.
        public async Task<IEnumerable<PizzaImager>> GetPizzasForUsersAsync()
        {
            var pizzasImager = new List<PizzaImager>();
            var pizzas = dbContext.Pizzas;
            var images = await dbContext.Images.ToListAsync();
            foreach (var pizza in pizzas)
            {
                var tempPath = images.FirstOrDefault(i => i.Id == pizza.ImageId).Path;
                var tempImageName = images.FirstOrDefault(i => i.Id == pizza.ImageId).Name;
                tempPath += tempImageName;
                var tempoPizza = new PizzaImager { PizzaId = pizza.Id,
                    PizzaName = pizza.Name, PizzaPrice = pizza.Price, ImagePath = tempPath};
                pizzasImager.Add(tempoPizza);
            }
            return pizzasImager;
        }
        public async Task<IEnumerable<Pizza>> GetPizzasForEditAsync()
        {
            var pizzas = await dbContext.Pizzas.ToListAsync();
            return pizzas;
        }
        // Получает пиццу, находящуюся в БД по id.
        public async Task<Pizza> GetPizzaAsync(int id)
        {
            var pizza = await dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id);
            return pizza;
        }
        public async Task SaveChangesAsync(Pizza pizza, int id, IFormFile uploadedFile)
        {
            var pizzaToChange = dbContext.Pizzas.Include(p => p.Image).
                FirstOrDefaultAsync(p => p.Id == id).Result;
            if (uploadedFile != null)
            {
                var addedImage = await AddImageFileAsync(uploadedFile);
                pizzaToChange.ImageId = addedImage.Id;
                // Если изображение больше не используется
                // ни для какой пиццы, происходит удаление
                // его из приложения и из базы данных.
                CheckImageUsing(pizzaToChange);
            }
            pizzaToChange.Name = pizza.Name;
            pizzaToChange.Price = pizza.Price;
            pizzaToChange.Diameter = pizza.Diameter;
            pizzaToChange.Novelty = pizza.Novelty;
            pizzaToChange.Bestseller = pizza.Bestseller;
            pizzaToChange.Discount = pizza.Discount;
            await dbContext.SaveChangesAsync();
        }
        // Удаляет пиццу из базы данных.
        public async Task DeleteAsync(int id)
        {
            var pizzaToDelete = await dbContext.Pizzas.Include(p => p.Image).
                FirstOrDefaultAsync(p => p.Id == id);
            // Удаляем саму пиццу из базы.
            dbContext.Pizzas.Remove(pizzaToDelete);
            await dbContext.SaveChangesAsync();
            // Логика для удаления лишних изображений.
            var otherPizzas = await dbContext.Pizzas.
                FirstOrDefaultAsync(p => p.Image.Id == pizzaToDelete.Image.Id);
            // Если изображение больше не используется
            // ни для какой пиццы, происходит удаление
            // его из приложения и из базы данных.
            if (otherPizzas == null)
            {
                CheckImageUsing(pizzaToDelete);
            }
            await dbContext.SaveChangesAsync();
        }
        // Проверяет, используется ли изображение 
        // для пицц в базе данных, и если нет - 
        // удаляет его из базы и из проекта.
        public void CheckImageUsing(Pizza pizzaToDelete)
        {
            DeleteImageFile(string.Concat(appEnvironment.WebRootPath,
                    pizzaToDelete.Image.Path, pizzaToDelete.Image.Name));
            var imageToDelete = dbContext.Images.
                FirstOrDefaultAsync(i => i.Id == pizzaToDelete.Image.Id).Result;
            dbContext.Images.Remove(imageToDelete);
        }
        public async Task AddNewPizzaAsync(Pizza pizza, Image image)
        {
            var newPizza = new Pizza()
            {
                Name = pizza.Name,
                Price = pizza.Price,
                Diameter = pizza.Diameter,
                Novelty = pizza.Novelty,
                Bestseller = pizza.Bestseller,
                Discount = pizza.Discount,
                ImageId = image.Id,
                Image = dbContext.Images.FirstOrDefaultAsync(i => i.Id == image.Id).Result
            };
            dbContext.Pizzas.Add(newPizza);
            await dbContext.SaveChangesAsync();
        }
        // Добавляет изображение в проект по указанному пути.
        public async Task<Image> AddImageFileAsync(IFormFile uploadedFile)
        {
            var fileName = uploadedFile.FileName;
            // Путь сохранения файла.
            var fullPathToFile = appEnvironment.WebRootPath + "/images/" + fileName;
            // В случае, если в директории уже содержится изображение
            // с таким названием, изменяем имя изображения(чтобы избежать автозамены файлов).
            fullPathToFile = FoldersManipulator.GetUniqueFilePath(fullPathToFile);
            using (var fileStream = new FileStream(fullPathToFile, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            var image = new Image { Name = Path.GetFileName(fullPathToFile), Path = "/images/" };
            dbContext.Images.Add(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
        // Удаляет изображение из проекта по указанному пути.
        public void DeleteImageFile(string pathToFile)
        {
            File.Delete(pathToFile);
        }
    }
} 
