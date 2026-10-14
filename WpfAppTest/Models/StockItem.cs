using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfAppTest.Models
{
    public class StockItem
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public double size { get; set; }

        [ForeignKey("product_id")]
        public virtual Product Product { get; set; } = null!;
    }
}
