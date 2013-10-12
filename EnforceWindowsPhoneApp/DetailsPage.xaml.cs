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
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Media.Imaging;

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
            ComplaintComments.ItemsSource = ComplaintCommentList;

            String responseContent = await Comment.GetComments("comments/" + tmpComplaint.Id);
            ComplaintComments.ItemsSource = null;
            
            ComplaintCommentList = Comment.parseList(responseContent);
            if (ComplaintCommentList.Count > 0)
            {
                IlkYorumButton.Visibility = Visibility.Collapsed;
                ComplaintComments.Visibility = Visibility.Visible;
                TumYorumlarButton.Visibility = Visibility.Visible;
                ComplaintComments.ItemsSource = ComplaintCommentList;
                TumYorumlarButton.Content = "Tüm Yorumları Görüntüle (" + ComplaintCommentList.Count + ")";
            }
            else
            {
                IlkYorumButton.Visibility = Visibility.Visible;
                ComplaintComments.Visibility = Visibility.Collapsed;
                TumYorumlarButton.Visibility = Visibility.Collapsed;
            }
            
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
            ComplaintMap.Center = new GeoCoordinate(tmpComplaint.Longitude, tmpComplaint.Latitude);
            //ComplaintMap.Center = new GeoCoordinate(39.916123, 32.8539379);
            //ComplaintMap.SetView(new GeoCoordinate(39.916123D, 32.8539379D), 17D);
            ComplaintMap.ZoomLevel = 16;

            Pushpin pushpin = new Pushpin();
            pushpin.GeoCoordinate = new GeoCoordinate(tmpComplaint.Longitude, tmpComplaint.Latitude);
            pushpin.Template = (ControlTemplate)(this.Resources["MyPushPinTemplate"]);

            MapOverlay pinOverlay = new MapOverlay();
            pinOverlay.Content = pushpin;
            pinOverlay.GeoCoordinate = new GeoCoordinate(tmpComplaint.Longitude, tmpComplaint.Latitude);

            MapLayer pinLayer = new MapLayer();
            pinLayer.Add(pinOverlay);
            ComplaintMap.Layers.Add(pinLayer);
        }
    }
}