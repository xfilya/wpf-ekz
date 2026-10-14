using Microsoft.EntityFrameworkCore;
using System.IO;
using WpfAppTest.Models;

namespace WpfAppTest.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> categories => Set<Category>();
        public DbSet<Product> products => Set<Product>();
        public DbSet<Manufacturer> manufacturers => Set<Manufacturer>();
        public DbSet<StockItem> stock_items => Set<StockItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dataBasePath = Path.Combine(AppContext.BaseDirectory, "database.db");

            if (File.Exists(dataBasePath)  == false){
                throw new Exception("Файл с БД не найден: " + dataBasePath);
            }

            optionsBuilder.UseSqlite("DataSource=" + dataBasePath);

            optionsBuilder.UseLazyLoadingProxies();
        }


    }
}
