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
    /// Interaction logic for ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        public CartItem item => DataContext as CartItem;
        public ItemPage(CartItem item)
        {
            var response = Global.client.Execute<int>(new RestRequest("cart/"+item.item.id));
            item.quantity = response.Data;
            DataContext = item;
            InitializeComponent();
        }

        private void Update_Quantity(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue == null)
                return;
            var request = new RestRequest("cart", Method.POST);
            request.AddParameter("id", item.item.id);
            request.AddParameter("quantity", e.NewValue);
            Global.client.Execute(request);
        }
    }
}
