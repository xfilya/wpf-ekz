using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfAppTest.Models
{
    public class Product
    {
        public int id {  get; set; }
        public string name { get; set; } = "";

        public int category_id { get; set; }
        public int manufacturer_id { get; set; }

        public string description { get; set; } = "";
        public string composition { get; set; } = "";
        public decimal price { get; set; }
        public string? image_file_name { get; set; }

        [ForeignKey("category_id")]
        public virtual Category Category { get; set; } = null!;


        [ForeignKey("manufacturer_id")]
        public virtual Manufacturer Manufacturer { get; set; } = null!;

        public virtual List<StockItem> StockItems { get; set; } = new List<StockItem>();
    }
}
