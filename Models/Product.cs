﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(100)]
        public string Brand { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [Precision(16, 2)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

    }
}
