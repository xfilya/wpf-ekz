using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using WpfAppTest.Models;
using WpfAppTest.Data;
using Microsoft.EntityFrameworkCore;

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (ApplicationContext db = new ApplicationContext())
            {
                List<Product> products = db.products
                    .Include(product => product.Category)
                    .Include(product => product.Manufacturer)
                    .Include(product => product.StockItems)
                    .ToList();

                products[0].image_file_name = "missing.png";

                List<ProductCardData> cards = CreateCards(products);

                ProductsList.ItemsSource = cards;
                ResultText.Text = $"Товаров в БД: {cards.Count}";
            }

            
        }

        private List<ProductCardData> CreateCards(List<Product> products)
        {
            const int ManyStockThreshold = 5;
            const int HighlightThreshold = 3;

            List<ProductCardData> cards = new List<ProductCardData>();

            foreach (Product product in products)
            {
                int totalQuantity = product.StockItems.Sum(stockItem => stockItem.quantity);

                ProductCardData card = new ProductCardData(product);

                if (totalQuantity > ManyStockThreshold)
                {
                    card.StockText = "много";
                }
                else
                {
                    card.StockText = "мало";
                }

                if (totalQuantity <= HighlightThreshold)
                {
                    card.BackgroundColor = "#FF8080";
                }
                else
                {
                    card.BackgroundColor = "#FFFFFF";
                }

                cards.Add(card);

            }

            return cards;
        }
    }
}