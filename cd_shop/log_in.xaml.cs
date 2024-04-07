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
        private void GetUserId(int userId)
        {
            string queryForAnId = "SELECT userId FROM Users WHERE userId = @UserId";
            dataBase.openConnection();
            SqlCommand sqlCommand = new SqlCommand(queryForAnId, dataBase.getConnection());
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            object result = sqlCommand.ExecuteScalar();
            if (result != null)
            {
                userId = Convert.ToInt32(result);
            }
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {

            string username = TextBox_login.Text;
            string password = TextBox_password.Password;
            string query = "SELECT userId, role_id FROM Users WHERE userLogin = @Username AND userPassword = @Password";

            dataBase.openConnection();

            SqlCommand command = new SqlCommand(query, dataBase.getConnection());

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int userId = reader.GetInt32(0);
                int roleId = reader.GetInt32(1);
                try
                {
                    dataBase.openConnection();
                   
                    switch (roleId)
                    {
                        case 1:
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.TextBox_UserLogin.Text = username;
                            mainWindow.TextBox_UserId.Text = userId.ToString();
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
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка авторизации: " + ex.Message);
                }
            }

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GuestWindowxaml Guestwindow = new GuestWindowxaml();
            Guestwindow.Show();
            this.Close();
        }
    }
}
