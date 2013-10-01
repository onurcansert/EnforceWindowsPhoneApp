﻿using System;
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

namespace EnforceWindowsPhoneApp
{
    public partial class LoginPage : PhoneApplicationPage
    {

        private String email;
        private String password;


        public LoginPage()
        {
            InitializeComponent();
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

        private void GirisYap_Click(object sender, EventArgs e)
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

            String url = "http://api.enforceapp.com/login";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), webRequest);
            
        }

        void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream postStream = webRequest.EndGetRequestStream(asynchronousResult);

            //String json = @"{""email"":""onur@onur.com"",""password"":""123""}";
            String json = "{\"email\" : \"" + email + "\", \"password\" : \"" + password + "\"}";

            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();
            webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);
        }

        void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                Stream streamResponse = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamResponse);

                String Response = streamReader.ReadToEnd();
                streamResponse.Close();
                streamReader.Close();
                response.Close();

                Dispatcher.BeginInvoke(() =>
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                });
            }
            catch (Exception e){
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Hatalı e-posta veya şifre.");
                });
            }
        }

    }
}