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
using SQLite;
using RestSharp;

namespace testapi
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SQLiteConnection sql;
        public static MainWindow window;
        public MainWindow()
        {
            window = this;
            Global.client = new RestClient("http://pavelflegr.8u.cz/eshop/");
            Global.client.CookieContainer = new System.Net.CookieContainer();
            InitializeComponent();
            CheckIfOffline();
            if (Global.username == null)
            {
                SetAnonymousMode();
            }
            if (Global.offline)
            {
                SetOfflineMode();
            }
            else
            {
                UpdateDB();
            }
            
            navigation.Navigate(new Items());
        }

        void CheckIfOffline()
        {
            var request = new RestRequest("", Method.GET);
            if (Global.client.Get(request).ResponseStatus == ResponseStatus.Error)
            {
                Global.offline = true;

            }
        }

        void UpdateDB()
        {
            sql = new SQLiteConnection("db");
            var response = Global.client.Execute<List<Item>>(new RestRequest("items"));
            List<Item> items = response.Data;
            sql.DropTable<Item>();
            sql.CreateTable<Item>();
            sql.InsertAll(items);
        }

        public void SetOfflineMode()
        {
            offlineIndicator.Visibility = Visibility.Visible;
            profileButton.IsEnabled = false;
            orderButton.IsEnabled = false;
            cartButton.IsEnabled = false;
        }

        public void SetAnonymousMode()
        {
            offlineIndicator.Visibility = Visibility.Collapsed;
            profileButton.IsEnabled = true;
            orderButton.IsEnabled = false;
            cartButton.IsEnabled = false;
        }
        public void SetLoggedMode()
        {
            offlineIndicator.Visibility = Visibility.Collapsed;
            profileButton.IsEnabled = true;
            orderButton.IsEnabled = true;
            cartButton.IsEnabled = true;
        }

        private void ShowProfile(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new Profile());
        }

        private void ShowOrders(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new Orders());
        }

        private void ShowItems(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new Items());
        }

        private void ShowCart(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new CartPage());
        }
    }
}
