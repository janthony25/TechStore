using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using TechStore.Repository.IRepository;

namespace TechStore.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _data;

        public ProductsRepository(DataContext data)
        {
            _data = data;
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
