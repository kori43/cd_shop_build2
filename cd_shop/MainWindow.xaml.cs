using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Collections.ObjectModel;
using System.Security.Policy;
using cd_shop.AddPages;


namespace cd_shop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void NewText_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(null);
            addWindow.Show();
            this.Close();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CDstoreEntities2.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGProducts.ItemsSource = CDstoreEntities2.GetContext().Products.ToList();
                DGUsers.ItemsSource = CDstoreEntities2.GetContext().Users.ToList();
                DGPublisher.ItemsSource = CDstoreEntities2.GetContext().Publishers.ToList();
                DGGenre.ItemsSource = CDstoreEntities2.GetContext().Genres.ToList();
                DGCategories.ItemsSource = CDstoreEntities2.GetContext().Categories.ToList();
            }
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow((sender as Button).DataContext as Product);
            addWindow.Show();
            this.Close();
        }


        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var delete = DGProducts.SelectedItems.Cast<Product>().ToList();
            if (delete.Count() > 0)
            {
                if (MessageBox.Show("Вы точно хотите удалить элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CDstoreEntities2.GetContext().Products.RemoveRange(delete);
                        CDstoreEntities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        DGProducts.ItemsSource = CDstoreEntities2.GetContext().Products.ToList();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Btn_Delete2_Click(object sender, RoutedEventArgs e)
        {
            var delete = DGUsers.SelectedItems.Cast<User>().ToList();
            if (delete.Count() > 0)
            {
                if (MessageBox.Show("Вы точно хотите удалить элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CDstoreEntities2.GetContext().Users.RemoveRange(delete);
                        CDstoreEntities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        DGUsers.ItemsSource = CDstoreEntities2.GetContext().Users.ToList();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Btn_Delete3_Click(object sender, RoutedEventArgs e)
        {
            var delete = DGPublisher.SelectedItems.Cast<Publisher>().ToList();
            if (delete.Count() > 0)
            {
                if (MessageBox.Show("Вы точно хотите удалить элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CDstoreEntities2.GetContext().Publishers.RemoveRange(delete);
                        CDstoreEntities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        DGPublisher.ItemsSource = CDstoreEntities2.GetContext().Publishers.ToList();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Btn_Delete4_Click(object sender, RoutedEventArgs e)
        {
            var delete = DGGenre.SelectedItems.Cast<Genre>().ToList();
            if (delete.Count() > 0)
            {
                if (MessageBox.Show("Вы точно хотите удалить элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CDstoreEntities2.GetContext().Genres.RemoveRange(delete);
                        CDstoreEntities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        DGGenre.ItemsSource = CDstoreEntities2.GetContext().Genres.ToList();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Btn_Delete5_Click(object sender, RoutedEventArgs e)
        {
            var delete = DGCategories.SelectedItems.Cast<Category>().ToList();
            if (delete.Count() > 0)
            {
                if (MessageBox.Show("Вы точно хотите удалить элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CDstoreEntities2.GetContext().Categories.RemoveRange(delete);
                        CDstoreEntities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        DGCategories.ItemsSource = CDstoreEntities2.GetContext().Categories.ToList();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            log_in log = new log_in();
            log.Show();
            this.Close();
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            sign_up sign_Up = new sign_up();
            sign_Up.Closed += (s, args) => { this.Show(); };
            sign_Up.Show();
            this.Hide();
        }

        private void GuestWindow_Click(object sender, RoutedEventArgs e)
        {
            GuestWindowxaml guestWindowxaml = new GuestWindowxaml();
            guestWindowxaml.Show();
            this.Close();
        }

        private void AddDopInfo_Click(object sender, RoutedEventArgs e)
        {
            AddWindowTwo addWindowTwo = new AddWindowTwo();
            addWindowTwo.Show();
            this.Close();
        }
    }
}
