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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;

namespace testapi
{
    /// <summary>
    /// Interakční logika pro Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
            var response1 = Global.client.Execute<List<Item>>(new RestRequest("items"));
            var items = response1.Data;
            var response2 = Global.client.Execute<List<Order>>(new RestRequest("orders"));
            var o = new List<object>();

            orders.ItemsSource = response2.Data;
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            var order = item.DataContext as Order;
            MainWindow.window.navigation.Navigate(new CartPage(order));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
