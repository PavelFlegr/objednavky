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
    /// Interakční logika pro Items.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public List<CartItem> itemList { get; set; }
        public bool enabled { get; set; } = true;
        int id = 0;
        public CartPage()
        {
            InitializeComponent();
            var response = Global.client.Execute<List<CartItem>>(new RestRequest("cart"));
            itemList = response.Data;
            items.ItemsSource = itemList;
        }

        public CartPage(Order order) : this()
        {
            text.Visibility = Visibility.Collapsed;
            enabled = false;
            itemList = order.items.Join(itemList, item1 => item1.id, item2 => item2.item.id, (item1, item2) => new CartItem(item2.item, item1.amount)).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = "orders";
            if (id != 0)
            {
                uri += "/" + id.ToString();
            }
            var list = itemList.Where(item => item.quantity > 0).Select(item => new { id = item.item.id, amount = item.quantity }).ToList();
            if(list.Count == 0)
            {
                MessageBox.Show("Vyberte alespoň 1 položku");
                return;
            }
            var request = new RestRequest(uri, Method.POST);
            request.AddJsonBody(list);
            Global.client.Execute(request);
            MainWindow.window.navigation.Navigate(new Orders());
        }

        private void Update_Quantity(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var control = sender as Xceed.Wpf.Toolkit.IntegerUpDown;
            var item = control.DataContext as CartItem;
            if (e.OldValue == null)
                return;
            var request = new RestRequest("cart", Method.POST);
            request.AddParameter("id", item.item.id);
            request.AddParameter("quantity", e.NewValue);
            Global.client.Execute(request);
        }
    }
}
