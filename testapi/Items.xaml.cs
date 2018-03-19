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
using SQLite;

namespace testapi
{
    /// <summary>
    /// Interakční logika pro Items.xaml
    /// </summary>
    public partial class Items : Page
    {
        public List<Item> itemList { get; set; }
        public bool enabled { get; set; } = true;
        int id = 0;
        public Items()
        {
            InitializeComponent();
            this.Loaded += Items_Loaded;
            GetItems();
        }

        void GetItems()
        {
            if(Global.offline)
            {
                var sql = new SQLiteConnection("db");
                sql.CreateTable<Item>();
                itemList = sql.Table<Item>().ToList();
            }
            else
            {
                var response = Global.client.Execute<List<Item>>(new RestRequest("items"));
                itemList = response.Data;
            }
        }

        private void Items_Loaded(object sender, RoutedEventArgs e)
        {
            items.ItemsSource = itemList;
        }

        private void Item_Description(object sender, MouseButtonEventArgs e)
        {
            var view = sender as ListView;
            var item = view.SelectedItem as Item;
            MainWindow.window.navigation.Navigate(new ItemPage(new CartItem(item)));
        }

        private void Prevent_Description(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
