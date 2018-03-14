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
            if(Global.username == null)
            {
                return;
            }
            login.Visibility = Visibility.Collapsed;
            profile.Visibility = Visibility.Visible;
            name.Text = Global.username;
            var response = Global.client.Execute<UserInfo>(new RestRequest("user", Method.GET));
            ulice.Text = response.Data.ulice;
            psc.Text = response.Data.psc;
            cp.Text = response.Data.cp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = Global.client.Execute(new RestRequest("logout", Method.POST));
            Global.username = null;
            MainWindow.window.navigation.Navigate(new Profile());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var request = new RestRequest("user", Method.POST);
            request.AddParameter("ulice", ulice.Text);
            request.AddParameter("psc", psc.Text);
            request.AddParameter("cp", cp.Text);
            Global.client.Execute(request);
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var request = new RestRequest("login", Method.POST);
            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(password.Password))
            {
                MessageBox.Show("Žádné pole nesmí být prázdné");
                return;
            }
            request.AddParameter("mail", username.Text);
            request.AddParameter("password", password.Password);
            var response = Global.client.Execute<bool>(request);
            if (!response.Data)
            {
                MessageBox.Show("Špatné uživatelské jméno nebo heslo");
            }
            else
            {
                Global.username = username.Text;
                MainWindow.window.navigation.Navigate(new Profile());
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var request = new RestRequest("register", Method.POST);
            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(password.Password))
            {
                MessageBox.Show("Žádné pole nesmí být prázdné");
                return;
            }
            request.AddParameter("mail", username.Text);
            request.AddParameter("password", password.Password);
            var response = Global.client.Execute<bool>(request);
            if (!response.Data)
            {
                MessageBox.Show("Účet s tímto přihlašovacím jménem už existuje");
            }
            else
            {
                request = new RestRequest("login", Method.POST);
                request.AddParameter("mail", username.Text);
                request.AddParameter("password", password.Password);
                Global.client.Execute(request);
                Global.username = username.Text;
            }
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login(null, null);
        }
    }
}
