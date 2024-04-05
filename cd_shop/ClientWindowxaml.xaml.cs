using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ClientWindowxaml.xaml
    /// </summary>
    public partial class ClientWindowxaml : Window
    {
        DataBase dataBase = new DataBase();
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        private ObservableCollection<CartItem> cart = new ObservableCollection<CartItem>();


        public ClientWindowxaml()
        {
            InitializeComponent();

            ProductsGrid.ItemsSource = products;
            CartGrid.ItemsSource = cart;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCart();
            LoadProducts();
        }

        public void LoadProducts()
        {
            try
            {
                products.Clear();
                dataBase.openConnection();
                string query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["productId"]),
                        ProductName = reader["productName"].ToString(),
                        PublisherId = Convert.ToInt32(reader["publisherId"]),
                        GenreId = Convert.ToInt32(reader["genreId"]),
                        CategoryId = Convert.ToInt32(reader["categoryId"]),
                        Price = Convert.ToDecimal(reader["price"]),
                        Count = Convert.ToInt32(reader["count"])
                    };
                    products.Add(product);
                }
                dataBase.closeConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке товаров: " + ex.Message);
            }
        }

        public void LoadCart()
        {
            try
            {
                cart.Clear();
                dataBase.openConnection();
                string query = "SELECT * FROM Cart";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CartItem cartItem = new CartItem
                    {
                        ProductId = Convert.ToInt32(reader["productId"]),
                        ProductName = reader["productName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                    cart.Add(cartItem);
                }
                dataBase.closeConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке корзины: " + ex.Message);
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CartItem selectedItem = CartGrid.SelectedItem as CartItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Выберите элемент для удаления из корзины.");
                    return;
                }
                int productId = selectedItem.ProductId;
                bool success = RemoveItemFromCart(productId);
                if (success)
                {
                    LoadCart();
                    MessageBox.Show("Товар успешно удален из корзины.");
                }
                else
                {
                    MessageBox.Show("Не удалось удалить товар из корзины.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении товара из корзины: " + ex.Message);
            }
        }

        private void Btn_AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = ProductsGrid.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите продукт для добавления в корзину.");
                return;
            }


            dataBase.openConnection();

            string query = "INSERT INTO Cart (productName, price) " +
                           "VALUES (@ProductName, @Price)";

            using (SqlCommand command = new SqlCommand(query, dataBase.getConnection()))
            {
                command.Parameters.AddWithValue("@ProductName", selectedProduct.ProductName);
                command.Parameters.AddWithValue("@Price", selectedProduct.Price);
                command.ExecuteNonQuery();
            }


            LoadCart();

        }       
        private bool RemoveItemFromCart(int productId)
        {
            try
            {

                var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    dataBase.openConnection();
                    string query = "DELETE FROM Cart WHERE ProductId = @ProductId";
                    using (var command = new SqlCommand(query, dataBase.getConnection()))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.ExecuteNonQuery();
                    }

                    dataBase.closeConnection();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при удалении элемента из корзины: " + ex.Message);
                return false;
            }
        }


        public class Product
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int PublisherId { get; set; }
            public int GenreId { get; set; }
            public int CategoryId { get; set; }
            public decimal Price { get; set; }
            public int Count { get; set; }
        }

        public class CartItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public CartItem()
            {

            }
        }

    }
}

