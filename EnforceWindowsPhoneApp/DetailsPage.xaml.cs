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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnforceWindowsPhoneApp
{
    public partial class DetailPage : PhoneApplicationPage
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            Complaint tmpComplaint = PhoneApplicationService.Current.State["Complaint"] as Complaint;    
            ComplaintTitle.Text = tmpComplaint.Title;
            ComplaintImage.Source = tmpComplaint.Source;
            ComplaintCategory.Text = tmpComplaint.Category;
        }
    }
}