using cd_shop.AddPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cd_shop
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private Product _currentProduct = new Product();
        public AddWindow(Product product)
        {
            InitializeComponent();

            if(product != null) 
            {
                _currentProduct = product;
            }

            DataContext = _currentProduct;
            PublisherBox.ItemsSource = CDstoreEntities2.GetContext().Publishers.ToList();
            GenreBox.ItemsSource = CDstoreEntities2.GetContext().Genres.ToList();
            CategoryBox.ItemsSource = CDstoreEntities2.GetContext().Categories.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentProduct.productName))
                errors.AppendLine("Укажите название продукта");
            if (_currentProduct.Publisher == null)
                errors.AppendLine("Укажите издателя");
            if (_currentProduct.Genre == null)
                errors.AppendLine("Укажите жанр");
            if (_currentProduct.Category == null)
                errors.AppendLine("Укажите категорию");
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }  
            if(_currentProduct.productId == 0)
            {
                CDstoreEntities2.GetContext().Products.Add(_currentProduct);
            }
            try
            {
                CDstoreEntities2.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NameProduct.Text = "";
            PublisherBox.Text = "";
            GenreBox.Text = "";
            CategoryBox.Text = "";
            Price.Text = "";
            Count.Text = "";
        }

        private void MenuPublisher_Click(object sender, RoutedEventArgs e)
        {
            AddWindowTwo addWindowTwo = new AddWindowTwo();
            addWindowTwo.Show();
            this.Close();
        }

        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
