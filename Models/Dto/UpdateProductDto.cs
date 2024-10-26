using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TechStore.Models.Dto
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }

        [MaxLength(100)]
        [DisplayName("Product Name")]
        public required string ProductName { get; set; }

        [MaxLength(100)]
        public required string Brand { get; set; }

        [MaxLength(50)]
        public required string Category { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        [DisplayName("Image")]
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }  
    }
}
