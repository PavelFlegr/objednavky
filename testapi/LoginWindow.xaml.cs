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
using System.Windows.Shapes;
using RestSharp;

namespace testapi
{
    /// <summary>
    /// Interakční logika pro LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        bool success = false;
        public LoginWindow()
        {
            InitializeComponent();
            var request = new RestRequest("", Method.GET);
            if(Global.client.Get(request).ResponseStatus == ResponseStatus.Error)
            {
                Global.offline = true;
                offlineIndicator.Visibility = Visibility.Visible;
            }
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
            if(!response.Data)
            {
                MessageBox.Show("Špatné uživatelské jméno nebo heslo");
            }   
            else
            {
                Global.username = username.Text;
                success = true;
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!success)
            {
                MainWindow.window.Close();
            }
            else
            {
                MainWindow.window.Show();
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
                success = true;
            }
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Login(null, null);
        }
    }
}
