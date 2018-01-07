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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            name.Text = Global.username;
            var response = Global.client.Execute<UserInfo>(new RestRequest("user", Method.GET));
            ulice.Text = response.Data.ulice;
            psc.Text = response.Data.psc;
            cp.Text = response.Data.cp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.window.Login();
            MainWindow.window.navigation.Navigate(new Items());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var request = new RestRequest("user", Method.POST);
            request.AddParameter("ulice", ulice.Text);
            request.AddParameter("psc", psc.Text);
            request.AddParameter("cp", cp.Text);
            Global.client.Execute(request);
        }
    }
}
