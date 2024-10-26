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
        private readonly ILogger<ProductsRepository> _logger;

        public ProductsRepository(DataContext data, IWebHostEnvironment environment, ILogger<ProductsRepository> logger)
        {
            _data = data;
            _environment = environment;
            _logger = logger;
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

        public async Task<UpdateProductDto> GetProductByIdAsync(int id)
        {
            try
            {

                var product = await _data.Products
                    .Where(p => p.ProductId == id)
                    .Select(p => new UpdateProductDto
                    {
                        ProductId= p.ProductId,
                        ProductName = p.ProductName,
                        Brand = p.Brand,
                        Category = p.Category,
                        Price = p.Price,
                        Description = p.Description
                    }).FirstOrDefaultAsync();


                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with id {id} not found");
                }

                return product;
               
            }
            catch (Exception ex)
            {
                // You could log the exception here if you have logging set up
                throw; // Rethrowing the exception to let it propagate
            }
        }

        public async Task UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            try
            {
                var product = await _data.Products.FindAsync(productDto.ProductId);
                if(product == null)
                {
                    _logger.LogWarning("Product not found");
                    throw new KeyNotFoundException($"Product with id {productDto.ProductId} not found");

                }

                // Only update the image file if a new one was uploaded
                if (productDto.ImageFile != null && productDto.ImageFile.Length > 0)
                {
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    newFileName += Path.GetExtension(productDto.ImageFile.FileName);

                    string imageFullPath = _environment.WebRootPath + "/photos/" + newFileName;
                    using (var stream = System.IO.File.Create(imageFullPath))
                    {
                        await productDto.ImageFile.CopyToAsync(stream);
                    }

                    //Only update the image filename if user saved a new image
                    product.ImageFileName = newFileName;
                }


               

                product.ProductName = productDto.ProductName;
                product.Brand = productDto.Brand;
                product.Category = productDto.Category;
                product.Price = productDto.Price;
                product.Description = productDto.Description;

                await _data.SaveChangesAsync();
            }
            catch(Exception ex)
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
