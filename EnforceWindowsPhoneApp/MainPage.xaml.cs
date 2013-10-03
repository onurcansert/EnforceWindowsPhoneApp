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
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading;

namespace EnforceWindowsPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<Complaint> complaintList = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DownloadComplaints("complaint/hot");
        }

        public async void DownloadComplaints(String url)
        {
            String responseContent = await Complaint.GetComplaints(url);
            ComplaintResults.ItemsSource = null;
            complaintList = Complaint.parseList(responseContent);
            ComplaintResults.ItemsSource = complaintList;
            DownloadComplaintImages();
        }

        public async void DownloadComplaintImages()
        {
            for (int i = 0; i < ComplaintResults.ItemsSource.Count; i++)
            {
                //complaintList[i].Source = new BitmapImage(new Uri("http://enforceapp.com" + complaintList[i].ImageURLs[0], UriKind.Absolute));
                complaintList[i].Source = await EnforceWindowsPhoneApp.Utils.Image.DownloadImage(complaintList[i].ImageURLs[0]);
                ComplaintResults.ItemsSource = null;
                ComplaintResults.ItemsSource = complaintList;
            }
        }


        private void HotList_Click(object sender, EventArgs e)
        {
            DownloadComplaints("complaint/hot");
        }

        private void RecentList_Click(object sender, EventArgs e)
        {
            DownloadComplaints("complaint/recent");
        }

        private void TopList_Click(object sender, EventArgs e)
        {
            DownloadComplaints("complaint/top");
        }

        private void Deneme_Click(object sender, EventArgs e)
        {
            String selectedComplaintId = (String)((System.Windows.Controls.Primitives.ButtonBase)(sender)).DataContext;
            int selectedComplaintIndex = -1;
            for (int i = 0; i < ComplaintResults.ItemsSource.Count; i++)
            {
                if (((Complaint)ComplaintResults.ItemsSource[i]).Id == selectedComplaintId)
                {
                    selectedComplaintIndex = i;
                }
            }
            PhoneApplicationService.Current.State["Complaint"] = (Complaint)ComplaintResults.ItemsSource[selectedComplaintIndex];
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));
        }
    }
}