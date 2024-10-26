using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;
using TechStore.Models.Dto;
using TechStore.Repository.IRepository;

namespace TechStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly DataContext _data;

        public ProductsController(IProductsRepository productsRepository, DataContext data)
        {
            _productsRepository = productsRepository;
            _data = data;
        }

        public async Task<IActionResult> GetAllProducts()
        {
            try
            {

                var products = await _productsRepository.GetAllProductsAsync();
                return View(products);
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while fetching product list.";
                return RedirectToAction("GetAllProducts");
            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProductDto productDto)
        {
            if(productDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            TempData["Success"] = "New product successfully added.";

            await _productsRepository.AddProductAsync(productDto);
            return RedirectToAction("GetAllProducts", "Products");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _productsRepository.GetProductByIdAsync(id);

                return View(product);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(productDto);
                }

                await _productsRepository.UpdateProductAsync(id, productDto);
                return RedirectToAction("GetAllProducts", "Products");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
