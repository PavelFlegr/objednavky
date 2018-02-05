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
        public List<ItemVM> itemList { get; set; }
        public bool enabled { get; set; } = true;
        int id = 0;
        public CartPage()
        {
            InitializeComponent();
            var response = Global.client.Execute<List<CartItem>>(new RestRequest("cart"));
            items.ItemsSource = response.Data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = "orders";
            if (id != 0)
            {
                uri += "/" + id.ToString();
            }
            var list = itemList.Where(item => item.amount > 0).Select(item => new { id = item.item.id, amount = item.amount }).ToList();
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
    }
}
