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
        public MainWindow()
        {
            Cookies.client = new RestSharp.RestClient("http://pavelflegr.8u.cz/eshop/");
            Cookies.client.CookieContainer = new System.Net.CookieContainer();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new Login());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new Orders());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            navigation.Navigate(new Items());
        }

        public void Modify(int id)
        {
            navigation.Navigate(new Items(id));
        }
    }
}
