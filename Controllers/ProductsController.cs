using Microsoft.AspNetCore.Mvc;
using TechStore.Repository.IRepository;

namespace TechStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
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
    }
}
