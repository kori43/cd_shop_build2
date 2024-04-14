using System.Linq;
using System.Windows;

namespace cd_shop
{
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
