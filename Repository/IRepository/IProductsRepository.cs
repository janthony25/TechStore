using TechStore.Models;
using TechStore.Models.Dto;

namespace TechStore.Repository.IRepository
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task AddProductAsync(AddProductDto productDto);
    }
}
