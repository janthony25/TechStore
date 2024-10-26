using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using TechStore.Data;
using TechStore.Models;
using TechStore.Models.Dto;
using TechStore.Repository.IRepository;

namespace TechStore.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _data;
        private readonly IWebHostEnvironment _environment;

        public ProductsRepository(DataContext data, IWebHostEnvironment environment)
        {
            _data = data;
            _environment = environment;
        }

        public async Task AddProductAsync(AddProductDto productDto)
        {
            try
            {

                // Creating path of the product image
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDto.ImageFile!.FileName);

                string imageFullPath = _environment.WebRootPath + "/photos/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDto.ImageFile.CopyTo(stream);
                }


                Product product = new Product()
                {
                    ProductName = productDto.ProductName,
                    Brand = productDto.Brand,
                    Category = productDto.Category,
                    Price = productDto.Price,
                    Description = productDto.Description,
                    ImageFileName = newFileName
                };

                _data.Products.AddAsync(product);
                await _data.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<List<Product>> IProductsRepository.GetAllProductsAsync()
        {
            try
            {
                // fetch all products
                return await _data.Products
                    .OrderByDescending(p => p.DateAdded)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
