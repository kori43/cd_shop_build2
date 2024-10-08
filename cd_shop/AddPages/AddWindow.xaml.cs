﻿using cd_shop.AddPages;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace cd_shop
{
    public partial class AddWindow : Window
    {
        private Product _currentProduct = new Product();
        public AddWindow(Product product)
        {
            InitializeComponent();

            if (product != null)
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
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentProduct.productId == 0)
            {
                CDstoreEntities2.GetContext().Products.Add(_currentProduct);
            }
            try
            {
                CDstoreEntities2.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Close();
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
    }
}
