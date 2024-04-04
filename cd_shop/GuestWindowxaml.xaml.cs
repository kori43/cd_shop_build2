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
    /// Логика взаимодействия для GuestWindowxaml.xaml
    /// </summary>
    public partial class GuestWindowxaml : Window
    {
        public GuestWindowxaml()
        {
            InitializeComponent();
            DGProducts.ItemsSource = CDstoreEntities2.GetContext().Products.ToList();
        }       

        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            log_in log_In = new log_in();
            log_In.Show();
            this.Close();
        }

        private void Btn_Regist_Click(object sender, RoutedEventArgs e)
        {
            sign_up sign_Up = new sign_up();
            sign_Up.Show();
            this.Close();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
