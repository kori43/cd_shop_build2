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

namespace cd_shop
{
    /// <summary>
    /// Логика взаимодействия для sign_up.xaml
    /// </summary>
    public partial class sign_up : Window
    {
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            log_in log_In = new log_in();
            log_In.Show();
            this.Close();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_login.Text = "";
            TextBox_password.Text = "";
            TextBox_passwordCheck.Text = "";
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if(checkUser())
            {
                return;
            }
            var login = TextBox_login.Text;
            var password = TextBox_password.Text;
            var passwordCheck = TextBox_passwordCheck.Text;
            if(password == passwordCheck) 
            {
                string querystring = $"insert into Users(userLogin, userPassword) values('{login}', '{password}')";

                SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

                dataBase.openConnection();

                if (login == "" && password == "")
                {
                    MessageBox.Show("Не удалось создать аккаунт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Аккаунт создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось создать аккаунт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                dataBase.closeConnection();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           

        }

        private Boolean checkUser()
        {
            var loginUser = TextBox_login.Text;
            var passUser = TextBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select * from Users where userLogin = '{loginUser}' and userPassword = '{passUser}'";
            
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0 ) 
            {
                MessageBox.Show("Такой аккаунт уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            else
            {
                return false;
            }    
        }
    }
}
