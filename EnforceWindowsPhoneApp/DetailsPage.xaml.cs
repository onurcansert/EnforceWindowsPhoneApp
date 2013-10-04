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
using System.Device.Location;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

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
            ComplaintAddress.Text = tmpComplaint.Address;

            List<Comment> ComplaintCommentList = new List<Comment>();
            Comment c1 = new Comment();
            c1.Id = "Onur";
            c1.Text = "Acayip güzel bir problem bu bayıldım ;)";

            Comment c2 = new Comment();
            c2.Id = "Onur";
            c2.Text = "Acayip güzel bir problem bu bayıldım ;)";

            ComplaintCommentList.Add(c1);
            ComplaintCommentList.Add(c2);

            ComplaintComments.ItemsSource = ComplaintCommentList;

            /*MapOverlay overlay = new MapOverlay
            {
                GeoCoordinate = ComplaintMap.Center,
                Content = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Red),
                    Width = 40,
                    Height = 40
                }
            };
            MapLayer layer = new MapLayer();
            layer.Add(overlay);

            ComplaintMap.Layers.Add(layer);*/

            //Geolocator locator = new Geolocator();            
            //ComplaintMap.Center = new GeoCoordinate(39.916123, 32.8539379);
            //ComplaintMap.SetView(new GeoCoordinate(39.916123D, 32.8539379D), 17D);
            //ComplaintMap.ZoomLevel = 3;
        }
    }
}