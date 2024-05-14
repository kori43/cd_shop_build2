using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using cd_shop.Classes;

namespace cd_shop
{
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
        private void LoadProducts()
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
                        productId = Convert.ToInt32(reader["productId"]),
                        productName = reader["productName"].ToString(),
                        publisherId = Convert.ToInt32(reader["publisherId"]),
                        genreId = Convert.ToInt32(reader["genreId"]),
                        categoryId = Convert.ToInt32(reader["categoryId"]),
                        price = Convert.ToDecimal(reader["price"]),
                        count = Convert.ToInt32(reader["count"])
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
        private void LoadCart()
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
            log_in log_In = new log_in();
            log_In.Show();
            this.Close();
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
                command.Parameters.AddWithValue("@ProductName", selectedProduct.productName);
                command.Parameters.AddWithValue("@Price", selectedProduct.price);
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
    }
}

