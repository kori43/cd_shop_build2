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
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace cd_shop
{
    /// <summary>
    /// Логика взаимодействия для log_in.xaml
    /// </summary>
    public partial class log_in : Window
    {
        DataBase dataBase = new DataBase();
        public log_in()
        {
            InitializeComponent();

        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            sign_up sign_Up = new sign_up();
            sign_Up.Show();
            this.Close();
        }


        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_login.Text = "";
            TextBox_password.Clear();

        }
        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {

            string username = TextBox_login.Text;
            string password = TextBox_password.Password;

            string query = "SELECT role_id FROM Users WHERE userLogin = @Username AND userPassword = @Password";

            dataBase.openConnection();

            SqlCommand command = new SqlCommand(query, dataBase.getConnection());
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            try
            {
                dataBase.openConnection();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    int role = Convert.ToInt32(result);

                    
                    switch (role)
                    {
                        case 1:
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            ClientWindowxaml clientWindowxaml = new ClientWindowxaml();
                            clientWindowxaml.Show();
                            this.Close();
                            break;
                        
                        default:
                            MessageBox.Show("Некорректная роль пользователя.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка авторизации: " + ex.Message);
            }



            //var loginUser = TextBox_login.Text;
            //var passwordUser = TextBox_password.Password;



            //SqlDataAdapter adapter = new SqlDataAdapter();
            //DataTable table = new DataTable();
            //string querystring = $"select userId, userLogin, userPassword, role_id from Users where userLogin = '{loginUser}' and userPassword = '{passwordUser}'";

            //SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());
            //adapter.SelectCommand = command;
            //adapter.Fill(table);


            //if (table.Rows.Count == 1)
            //{

            //    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    MainWindow mainWindow = new MainWindow();
            //    mainWindow.Show();
            //    this.Close();

            //}

            //else
            //{
            //    MessageBox.Show("Такого аккаунта не существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GuestWindowxaml Guestwindow = new GuestWindowxaml();
            Guestwindow.Show();
            this.Close();
        }
    }
}
