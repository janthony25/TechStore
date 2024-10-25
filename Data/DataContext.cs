using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
