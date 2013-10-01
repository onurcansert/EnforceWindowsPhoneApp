using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EnforceWindowsPhoneApp.Resources;
using EnforceWindowsPhoneApp.Utils;

namespace EnforceWindowsPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            String url = "http://api.enforceapp.com/complaint/hot";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += dowloadComplaintListFromAPI;
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
        }

        private void dowloadComplaintListFromAPI(object sender, DownloadStringCompletedEventArgs e)
        {
            String jsonResult = e.Result;
            ComplaintResults.ItemsSource = null;
            ComplaintResults.ItemsSource = Complaint.parseList(jsonResult);
        }

        private void HotList_Click(object sender, EventArgs e)
        {
            String url = "http://api.enforceapp.com/complaint/hot";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += dowloadComplaintListFromAPI;
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
        }

        private void RecentList_Click(object sender, EventArgs e)
        {
            String url = "http://api.enforceapp.com/complaint/recent";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += dowloadComplaintListFromAPI;
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
        }

        private void TopList_Click(object sender, EventArgs e)
        {
            String url = "http://api.enforceapp.com/complaint/top";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += dowloadComplaintListFromAPI;
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
        }
    }
}