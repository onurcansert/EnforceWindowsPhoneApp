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
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace EnforceWindowsPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        ObservableCollection<Complaint> complaintList = null;
        private String status;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            /*SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 0.5);
            SystemTray.SetBackgroundColor(this, Colors.Purple);
            SystemTray.SetForegroundColor(this, Colors.Yellow);

            ProgressIndicator progress = new ProgressIndicator
            {
                IsVisible = true,
                IsIndeterminate = true,
                Text = "Click me..."
            };

            SystemTray.SetProgressIndicator(this, progress);*/

            status = "NewLoad";
            DownloadComplaints("complaint/hot");
        }

        public async void DownloadComplaints(String url)
        {
            String responseContent = await Complaint.GetComplaints(url);
            complaintList = Complaint.parseList(responseContent);

            //ComplaintResults.ItemsSource = null;
            if (status == "NewLoad")
            {
                ComplaintResults.ItemsSource = complaintList;
                DownloadComplaintImages();
            }
            else
            {
                ObservableCollection<Complaint> tmpComplaintList = (ObservableCollection<Complaint>)ComplaintResults.ItemsSource;
                for (int i = 0; i < complaintList.Count; i++)
                {
                    ComplaintResults.ItemsSource.Add(complaintList[i]);
                }
                //ComplaintResults.ItemsSource = null;
                //ComplaintResults.ItemsSource = tmpComplaintList;
            }
            DownloadComplaintImages();
        }

        public async void DownloadComplaintImages()
        {
            for (int i = 0; i < ComplaintResults.ItemsSource.Count; i++)
            {
                //complaintList[i].Source = new BitmapImage(new Uri("http://enforceapp.com" + complaintList[i].ImageURLs[0], UriKind.Absolute));
                //complaintList[i].Source = await EnforceWindowsPhoneApp.Utils.Image.DownloadImage(complaintList[i].ImageURLs[0]);
                ((Complaint)ComplaintResults.ItemsSource[i]).Source = await EnforceWindowsPhoneApp.Utils.Image.DownloadImage(((Complaint)ComplaintResults.ItemsSource[i]).ImageURLs[0]);
                //ComplaintResults.ItemsSource = complaintList;
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

        private void ComplaintMore_Click(object sender, EventArgs e)
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

        private void NewComplaint_Click(object sender, EventArgs e)
        {
            //PhoneApplicationService.Current.State["Complaint"] = (Complaint)ComplaintResults.ItemsSource[selectedComplaintIndex];
            NavigationService.Navigate(new Uri("/Deneme.xaml", UriKind.Relative));
        }

        void ComplaintResults_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (e.ItemKind == LongListSelectorItemKind.Item & ComplaintResults.ItemsSource != null)
            {
                if (ComplaintResults.ItemsSource[ComplaintResults.ItemsSource.Count - 1].Equals(e.Container.Content as Complaint))
                {
                    status = "LazyLoad";
                    Complaint tmpComplaint = (Complaint) ComplaintResults.ItemsSource[ComplaintResults.ItemsSource.Count-1];
                    DownloadComplaints("complaint/hot?sinceid=" + tmpComplaint.Id);
                    this.LayoutRoot.UpdateLayout();
                }
            }
        } 

    }
}