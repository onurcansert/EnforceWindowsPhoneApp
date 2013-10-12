using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace EnforceWindowsPhoneApp.Utils
{
    class Complaint
    {
        public String Id { get; set; }
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public User Reporter { get; set; }
        public String Category { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public List<String> UpVoters { get; set; }
        public List<String> DownVoters { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public List<String> ImageURLs { get; set; }
        public int CommentsCount { get; set; }
        public String PublicURL { get; set; }
        public String SlugURL { get; set; }

        public String capturedImageByteArrayBase64 { get; set; }

        public System.Windows.Media.ImageSource Source { get; set; }

        public static ObservableCollection<Complaint> parseList(String jsonResult)
        {
            Object obj = JsonConvert.DeserializeObject(jsonResult);
            JContainer jContainer = (JContainer)(((JArray)(obj)));

            ObservableCollection<Complaint> complaints = new ObservableCollection<Complaint>();
            for (int i = 0; i < jContainer.Count; i++)
            {
                Complaint complaint = new Complaint();
                complaint = fromJSON((JObject)jContainer[i]);
                complaints.Add(complaint);
            }
            return complaints;
        }

        private static Complaint complaint = null;

        public static Complaint fromJSON(JObject jObjectComplaint)
        {            
            complaint = new Complaint();

            complaint.Id = (String)jObjectComplaint["_id"];
            complaint.Address = (String)jObjectComplaint["address"];
            complaint.Category = (String)jObjectComplaint["category"];
            complaint.City = (String)jObjectComplaint["city"];
            complaint.Title = (String)jObjectComplaint["title"];
            complaint.SlugURL = (String)jObjectComplaint["slug_url"];
            complaint.PublicURL = (String)jObjectComplaint["public_url"];

            complaint.UpVote = Int32.Parse((String)jObjectComplaint["upvote_count"]);
            complaint.DownVote = Int32.Parse((String)jObjectComplaint["downvote_count"]);
            complaint.CommentsCount = Int32.Parse((String)jObjectComplaint["comments_count"]);

            complaint.Longitude = Double.Parse((String)jObjectComplaint["location"][0]);
            complaint.Latitude = Double.Parse((String)jObjectComplaint["location"][1]);

            complaint.Date = Convert.ToDateTime((String)jObjectComplaint["date"]);

            complaint.UpVoters = jObjectComplaint["upvoters"].ToObject<List<String>>();
            complaint.DownVoters = jObjectComplaint["downvoters"].ToObject<List<String>>();
            complaint.ImageURLs = jObjectComplaint["pics"].ToObject<List<String>>();

            if (complaint.ImageURLs.Count > 0)
            {
                //complaint.Source = new BitmapImage(new Uri("http://enforceapp.com" + complaint.ImageURLs[0], UriKind.Absolute));
                //BitmapImage(new Uri("http://enforceapp.com" + complaint.ImageURLs[0], UriKind.Absolute));

                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(Application.GetResourceStream(new Uri(@"Assets/loading.png", UriKind.Relative)).Stream);
                complaint.Source = bmp;
                
                //var res = Image.DownloadImage(complaint.ImageURLs[0]);
                /*WebClient wc = new WebClient();
                wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
                wc.OpenReadAsync(new Uri("http://enforceapp.com" + complaint.ImageURLs[0], UriKind.Absolute), wc);*/
            }
            else
            {
                //complaint.Source = new BitmapImage(new Uri("ms-appx:///Assets/loading.png", UriKind.Absolute));
            }

            User reporter = new User();
            Newtonsoft.Json.Linq.JObject jObjectUser = (Newtonsoft.Json.Linq.JObject)jObjectComplaint["user"];
            reporter.Id = (String)jObjectUser["_id"];
            reporter.Name = (String)jObjectUser["name"];
            reporter.Email = (String)jObjectUser["email"];

            complaint.Reporter = reporter;

            return complaint;
        }

        public String toJson()
        {
            JObject jObjectComplaint = new JObject();
            try
            {
                jObjectComplaint.Add("title", this.Title);
                jObjectComplaint.Add("category", this.Category);
                jObjectComplaint.Add("city", this.City);
                jObjectComplaint.Add("address", this.Address);                

                JArray jArrayGeo = new JArray();
                jArrayGeo.Add(this.Latitude);
                jArrayGeo.Add(this.Longitude);
                jObjectComplaint.Add("location", jArrayGeo);
                jObjectComplaint.Add("pic", capturedImageByteArrayBase64);
            }
            catch (Exception e) { }
            return jObjectComplaint.ToString();
        }

        public static void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                try
                {
                    BitmapImage image = new BitmapImage();
                    image.SetSource(e.Result);
                    complaint.Source = image;
                }
                catch (Exception ex) { }
            }
            else
            {
                //Either cancelled or error handle appropriately for your app
            }
        }

        public async static Task<String> GetComplaints(String url)
        {
            String responseContent = await Request.Get(url);
            return responseContent;
        }

        //public System.Windows.Media.ImageSource Source { get {return this.Source; } set { this.Source = new BitmapImage(new Uri("ms-appx:///Assets/loading.png")); } }

        //public List<Comment> comments = null;
        //public final List<Image> images = new ArrayList<Image>();
        
        /*public void setId(String id)
        {
            this.id = id;
        }

        public String getId()
        {
            return id;
        }

        public void setTitle(String title)
        {
            this.title = title;
        }

        public String getTitle()
        {
            return title;
        }

        public void setCategory(String category)
        {
            this.category = category;
        }

        public String getCategory()
        {
            return category;
        }

        public void setUpVote(int upVote)
        {
            this.upVote = upVote;
        }

        public int getUpVote()
        {
            return upVote;
        }

        public void setDownVote(int downVote)
        {
            this.downVote = downVote;
        }

        public int getDownVote()
        {
            return downVote;
        }

        public void setUpVoters(List<String> upvoters)
        {
            this.upvoters = upvoters;
        }

        public List<String> getUpVoters()
        {
            return upvoters;
        }

        public void setDownVoters(List<String> downvoters)
        {
            this.downvoters = downvoters;
        }

        public List<String> getDownVoters()
        {
            return downvoters;
        }

        public void setAddress(String address)
        {
            this.address = address;
        }

        public String getAddres()
        {
            return address;
        }

        public void setCity(String city)
        {
            this.city = city;
        }

        public String getCity()
        {
            return city;
        }

        public void setLongitude(double longitude)
        {
            this.longitude = longitude;
        }

        public double getLongitude()
        {
            return longitude;
        }

        public void setLatitude(double latitude)
        {
            this.latitude = latitude;
        }

        public double getLatitude()
        {
            return latitude;
        }

        public void setDate(DateTime date)
        {
            this.date = date;
        }

        public DateTime getDate()
        {
            return date;
        }

        public void setCommentsCount(int comments_count)
        {
            this.comments_count = comments_count;
        }

        public int getCommentsCount()
        {
            return comments_count;
        }

        public void setSlugURL(String slug_URL)
        {
            this.slug_URL = slug_URL;
        }

        public String getSlugURL()
        {
            return slug_URL;
        }

        public void setPublicURL(String public_URL)
        {
            this.public_URL = public_URL;
        }

        public String getPublicURL()
        {
            return public_URL;
        }

        public void setImageURLs(List<String> imageURLs)
        {
            this.imageURLs = imageURLs;
        }

        public List<String> getImageURLs()
        {
            return imageURLs;
        }

        public void setReporter(User reporter)
        {
            this.reporter = reporter;
        }

        public User getReporter()
        {
            return reporter;
        }*/
    }
}
