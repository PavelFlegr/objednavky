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
            foreach(var order in response2.Data)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in order.items)
                {
                    sb.Append(items.First(i => i.id == item.id).name);
                    sb.Append(", ");
                }
                var text = sb.ToString();
                if (text.Length > 0)
                {
                    text = text.Substring(0, text.Length - 2);
                }
                o.Add(new OrderItem2{ text = text, id = order.id });
            }

            orders.ItemsSource = o;


        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            var orderItem = item.DataContext as OrderItem2;
            ((MainWindow)Application.Current.MainWindow).Modify(orderItem.id);
        }
    }
}
