using TechStore.Models;
using TechStore.Models.Dto;

namespace TechStore.Repository.IRepository
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task AddProductAsync(AddProductDto productDto);
        Task<UpdateProductDto> GetProductByIdAsync(int id);
        Task UpdateProductAsync(int id, UpdateProductDto productDto);
    }
}
