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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var request = new RestRequest("login", Method.POST);
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
                Application.Current.MainWindow.Close();
            }
            else
            {
                Application.Current.MainWindow.Show();
            }
        }
    }
}
