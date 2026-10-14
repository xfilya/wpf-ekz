using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WpfAppTest.Models
{
    public class ProductCardData
    {
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public string StockText { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public ProductCardData(Product product) 
        {
            Product = product;
            Price = product.price;
            ImagePath = GetImagePath(product);
        }

        private string GetImagePath(Product product)
        {
            string fallbackPath = Path.Combine(AppContext.BaseDirectory, "Assets", "picture.png");
            string? fileName = product.image_file_name;

            if (string.IsNullOrEmpty(fileName))
            {
                return fallbackPath;
            }

            string imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", "Images", fileName);

            if (File.Exists(imagePath))
            {
                return imagePath;
            }
            else
            {
                return fallbackPath;
            }
        }
    }
}
