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

namespace testapi
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow window;
        public MainWindow()
        {
            window = this;
            Global.client = new RestSharp.RestClient("http://pavelflegr.8u.cz/eshop/");
            Global.client.CookieContainer = new System.Net.CookieContainer();
            Initialized += LoginEvent;
            InitializeComponent();
            
        }

        private void LoginEvent(object sender, EventArgs e)
        {
            Login();
        }

        public void Login()
        {
            Hide();
            var window = new LoginWindow();
            window.Show();
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

        public void Modify(int id)
        {
            navigation.Navigate(new Items(id));
        }
    }
}
