﻿using System;
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

namespace cd_shop.AddPages
{

    public partial class AddWindowTwo : Window
    {
        DataBase dataBase = new DataBase();

        public AddWindowTwo()
        {
            InitializeComponent();

        }

        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            dataBase.openConnection();

            var publisher = NamePublisher.Text;
            var genre = NameGenre.Text;
            var category = NameCategory.Text;

            if (NamePublisher.Text != "")
            {
                try
                {
                    var addQuery = $"insert into Publisher (publisherName) values ('{publisher}')";

                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Издатель успешно добавлен!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (NameGenre.Text != "")
            {
                try
                {
                    var addQuery = $"insert into Genre (genreName) values ('{genre}')";

                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Жанр успешно добавлен!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            if (NameCategory.Text != "")
            {
                try
                {
                    var addQuery = $"insert into Categories (categoryName) values ('{category}')";

                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Категория успешно добавлена!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dataBase.closeConnection();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NamePublisher.Text = "";
            NameGenre.Text = "";
            NameCategory.Text = "";
        }

        private void ProductAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(null);
            addWindow.Show();
            this.Close();
        }
      
    }
}
