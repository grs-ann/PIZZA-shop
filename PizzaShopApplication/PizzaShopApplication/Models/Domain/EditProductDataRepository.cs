using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Secondary;
using PizzaShopApplication.Models.ProductModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain
{
    /// <summary>
    /// This class provides possibility to use CRUD opertaions
    /// with products, containing in database.
    /// </summary>
    public class EditProductDataRepository
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;
        public EditProductDataRepository(ApplicationDataContext dbContext,
            IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
        }
        /// <summary>
        /// Adds a new product with pizza type to database table.
        /// </summary>
        /// <param name="pizza">Pizza model, which containes added pizza data in View.</param>
        /// <param name="image">Uploaded image.</param>
        public async Task AddNewPizzaAsync(PizzaViewModel pizza, Image image)
        {
            _dbContext.Products.Add(
                new Product()
                {
                    Name = pizza.Name,
                    Price = pizza.Price,
                    Novelty = pizza.Novelty,
                    Bestseller = pizza.Bestseller,
                    Discount = pizza.Discount,
                    ImageId = image.Id,
                    Image = _dbContext.Images.FirstOrDefaultAsync(i => i.Id == image.Id).Result,
                    ProductTypeId = 1,
                    ProductProperties = new List<ProductProperty>
                    {
                        new ProductProperty
                        {
                            Value = pizza.PizzaIngridients,
                            PropertyId = 1
                        }
                    }
                });
            await _dbContext.SaveChangesAsync();

        }
        /// <summary>
        /// Adds a new product with drink type to database table.
        /// </summary>
        /// <param name="pizza">Drink model, which containes added drink data in View.</param>
        /// <param name="image">Uploaded image.</param>
        public async Task AddNewDrinkAsync(DrinkViewModel product, Image image)
        {
            _dbContext.Products.Add(
                new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Novelty = product.Novelty,
                    Bestseller = product.Bestseller,
                    Discount = product.Discount,
                    ImageId = image.Id,
                    Image = _dbContext.Images.FirstOrDefaultAsync(i => i.Id == image.Id).Result,
                    ProductTypeId = 2
                });
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Changes product data with pizza type in database table.
        /// </summary>
        /// <param name="pizzaModel">Pizza model, which containes added pizza data in View.</param>
        /// <param name="uploadedFile">Uploaded image.</param>
        public async Task EditPizzaAsync(PizzaViewModel pizzaModel, IFormFile uploadedFile)
        {
            var pizzaToChange = await _dbContext.Products.Include(p => p.ProductProperties)
                        .Include(p => p.Image)
                        .FirstOrDefaultAsync(p => p.Id == pizzaModel.Id);
            if (uploadedFile != null)
            {
                var addedImage = await AddImageFileAsync(uploadedFile);
                pizzaToChange.ImageId = addedImage.Id;
                CheckImageUsing(pizzaToChange);
            }
            pizzaToChange.Name = pizzaModel.Name;
            pizzaToChange.Price = pizzaModel.Price;
            pizzaToChange.ProductProperties
                .FirstOrDefault(p => p.Id == pizzaToChange.Id)
                .Value = pizzaModel.PizzaIngridients;
            pizzaToChange.Novelty = pizzaModel.Novelty;
            pizzaToChange.Bestseller = pizzaModel.Bestseller;
            pizzaToChange.Discount = pizzaModel.Discount;
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Changes product data with drink type in database table.
        /// </summary>
        /// <param name="pizzaModel">Drink model, which containes added drink data in View.</param>
        /// <param name="uploadedFile">Uploaded image.</param>
        public async Task EditDrinkAsync(DrinkViewModel drinkModel, IFormFile uploadedFile)
        {
            var drinkToChange = await _dbContext.Products.Include(p => p.ProductProperties)
                        .Include(p => p.Image)
                        .FirstOrDefaultAsync(p => p.Id == drinkModel.Id);
            if (uploadedFile != null)
            {
                var addedImage = await AddImageFileAsync(uploadedFile);
                drinkToChange.ImageId = addedImage.Id;
                CheckImageUsing(drinkToChange);
            }
            drinkToChange.Name = drinkModel.Name;
            drinkToChange.Price = drinkModel.Price;
            drinkToChange.Novelty = drinkModel.Novelty;
            drinkToChange.Bestseller = drinkModel.Bestseller;
            drinkToChange.Discount = drinkModel.Discount;
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes product from database by product Id.
        /// </summary>
        /// <param name="id">Product Id</param>
        public async Task DeleteProductAsync(int id)
        {
            var productToDelete = await _dbContext.Products.Include(p => p.Image).
                FirstOrDefaultAsync(p => p.Id == id);
            // Delete product from database.
            _dbContext.Products.Remove(productToDelete);
            await _dbContext.SaveChangesAsync();
            
            var otherProducts = await _dbContext.Products.
                FirstOrDefaultAsync(p => p.Image.Id == productToDelete.Image.Id);
            // Removing unused images 
            if (otherProducts == null)
            {
                CheckImageUsing(productToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Checks if the image is used for other products and,
        /// if not, removes it from the project and from the database. 
        /// </summary>
        /// <param name="productToDelete">Product to be removed</param>
        public void CheckImageUsing(Product productToDelete)
        {
            File.Delete(string.Concat(_appEnvironment.WebRootPath,
                    productToDelete.Image.Path, productToDelete.Image.Name));
            var imageToDelete = _dbContext.Images.
                FirstOrDefaultAsync(i => i.Id == productToDelete.Image.Id).Result;
            _dbContext.Images.Remove(imageToDelete);
        }
        /// <summary>
        /// Adds a new image in project and 
        /// database by the specified path.
        /// </summary>
        /// <param name="uploadedFile">Image, uploaded by user</param>
        /// <returns></returns>
        public async Task<Image> AddImageFileAsync(IFormFile uploadedFile)
        {
            var fileName = uploadedFile.FileName;
            // Path to save.
            var fullPathToFile = _appEnvironment.WebRootPath + "/images/" + fileName;
            // If the directory already contains an image with the same name, 
            // change the name of the image to avoid automatic replacement
            // of files with the same name. 
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
