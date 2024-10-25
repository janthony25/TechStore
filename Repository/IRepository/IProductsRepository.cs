using TechStore.Models;

namespace TechStore.Repository.IRepository
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
