using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using EnforceWindowsPhoneApp.Utils;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EnforceWindowsPhoneApp
{
    public partial class LoginPage : PhoneApplicationPage
    {

        private String email;
        private String password;


        public LoginPage()
        {
            InitializeComponent();
            LoggedInControl();
        }

        private void dowloadUserInfoFromAPI(object sender, DownloadStringCompletedEventArgs e)
        {
            String jsonResult = e.Result;
            
        }

        private void EMailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EMailTextBox.Text == "E-Posta")
            {
                EMailTextBox.Text = "";
                //SolidColorBrush Brush1 = new SolidColorBrush();
                //Brush1.Color = Colors.Magenta;
                //WatermarkTB.Foreground = Brush1;
            }
        }

        private void EMailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EMailTextBox.Text == "")
            {
                EMailTextBox.Text = "E-Posta";
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Text == "Şifre")
            {
                PasswordTextBox.Text = "";
                //SolidColorBrush Brush1 = new SolidColorBrush();
                //Brush1.Color = Colors.Magenta;
                //WatermarkTB.Foreground = Brush1;
            }
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Text == "")
            {
                PasswordTextBox.Text = "Şifre";
            }
        }

        private async void GirisYap_Click(object sender, EventArgs e)
        {
            email = EMailTextBox.Text;
            password = PasswordTextBox.Text;

            if (email == "" || email == "E-Posta") {
                MessageBox.Show("Lütfen e-posta hesabınızı giriniz.");
                return;
            }
            if (password == "" || password == "Şifre") { 
                MessageBox.Show("Lütfen şifrenizi giriniz.");
                return;
            }

            LoginPanel.Visibility = Visibility.Collapsed;
            LoadingPanel.Visibility = Visibility.Visible;

            //String json = @"{""email"":""onur@onur.com"",""password"":""123""}";
            String json = "{\"email\" : \"" + email + "\", \"password\" : \"" + password + "\"}";
            String responseLogin = await User.Login(json);
            if (responseLogin != null && responseLogin != "")
            {
                User loggedInUser = User.parseUser(responseLogin);                
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Lütfen kullanıcı adınızı ve şifrenizi kontrol ediniz.");
                LoginPanel.Visibility = Visibility.Visible;
                LoadingPanel.Visibility = Visibility.Collapsed;
            }
        }

    }
}