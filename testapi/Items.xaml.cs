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
    public partial class Items : Page
    {
        int id = 0;
        public Items()
        {
            InitializeComponent();
            var response = Global.client.Execute<List<Item>>(new RestRequest("items"));
            items.ItemsSource = response.Data;
        }

        public Items(int id) : this()
        {
            this.id = id;
            text.Content = "Změnit";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = "orders";
            if (id != 0)
            {
                uri += "/" + id.ToString();
            }
            var selected = items.SelectedItems.Cast<Item>().ToList();
            var list = selected.Select(item => new { id = item.id, amount = 1 }).ToList();
            var request = new RestRequest(uri, Method.POST);
            request.AddJsonBody(list);
            Global.client.Execute(request);
        }
    }
}
