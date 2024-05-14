using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

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
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.TextBox_UserLogin.Text = username;
                            adminWindow.TextBox_UserId.Text = userId.ToString();
                            adminWindow.Show();
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
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Ошибка");
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
