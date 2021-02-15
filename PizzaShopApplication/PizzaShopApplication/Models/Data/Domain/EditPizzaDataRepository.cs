using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain
{
    public class EditPizzaDataRepository
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;
        public EditPizzaDataRepository(ApplicationDataContext dbContext,
            IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
        }
        // Добавляет новую пиццу в базу данных.
        public async Task AddNewPizzaAsync(Pizza pizza, Image image)
        {
            _dbContext.Pizzas.Add(
                new Pizza()
                {
                    Name = pizza.Name,
                    Price = pizza.Price,
                    Diameter = pizza.Diameter,
                    Novelty = pizza.Novelty,
                    Bestseller = pizza.Bestseller,
                    Discount = pizza.Discount,
                    ImageId = image.Id,
                    Image = _dbContext.Images.FirstOrDefaultAsync(i => i.Id == image.Id).Result
                });
            await _dbContext.SaveChangesAsync();
        }
        // Изменяет информацию о пицце.
        public async Task EditPizzaAsync(Pizza pizza, int id, IFormFile uploadedFile)
        {
            var pizzaToChange = _dbContext.Pizzas.Include(p => p.Image).
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
            await _dbContext.SaveChangesAsync();
        }
        // Удаляет пиццу из базы данных.
        public async Task DeletePizzaAsync(int id)
        {
            var pizzaToDelete = await _dbContext.Pizzas.Include(p => p.Image).
                FirstOrDefaultAsync(p => p.Id == id);
            // Удаляем саму пиццу из базы.
            _dbContext.Pizzas.Remove(pizzaToDelete);
            await _dbContext.SaveChangesAsync();
            // Логика для удаления лишних изображений.
            var otherPizzas = await _dbContext.Pizzas.
                FirstOrDefaultAsync(p => p.Image.Id == pizzaToDelete.Image.Id);
            // Если изображение больше не используется
            // ни для какой пиццы, происходит удаление
            // его из приложения и из базы данных.
            if (otherPizzas == null)
            {
                CheckImageUsing(pizzaToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }
        // Проверяет, используется ли изображение 
        // для пицц в базе данных, и если нет - 
        // удаляет его из базы и из проекта.
        public void CheckImageUsing(Pizza pizzaToDelete)
        {
            File.Delete(string.Concat(_appEnvironment.WebRootPath,
                    pizzaToDelete.Image.Path, pizzaToDelete.Image.Name));
            var imageToDelete = _dbContext.Images.
                FirstOrDefaultAsync(i => i.Id == pizzaToDelete.Image.Id).Result;
            _dbContext.Images.Remove(imageToDelete);
        }
        // Добавляет изображение в проект по указанному пути, а 
        // также сохраняет данные о изображении в базу данных.
        public async Task<Image> AddImageFileAsync(IFormFile uploadedFile)
        {
            var fileName = uploadedFile.FileName;
            // Путь сохранения файла.
            var fullPathToFile = _appEnvironment.WebRootPath + "/images/" + fileName;
            // В случае, если в директории уже содержится изображение
            // с таким названием, изменяем имя изображения, чтобы избежать 
            // автозамены файлов с одинаковым названием.
            fullPathToFile = FoldersManipulator.GetUniqueFilePath(fullPathToFile);
            using (var fileStream = new FileStream(fullPathToFile, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            var image = new Image { Name = Path.GetFileName(fullPathToFile), Path = "/images/" };
            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();
            return image;
        }
    }
}
