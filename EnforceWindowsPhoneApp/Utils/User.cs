using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnforceWindowsPhoneApp.Utils
{
    class User
    {
        public String Id { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }        
        public String UserSlug { get; set; }
        public String UserType { get; set; }
        public int Facebook { get; set; }

        public System.Windows.Media.ImageSource Avatar { get; set; }

        public User()
        {
            Id = "-1";
            Email = "";
            Name = "";
        }

        public User(String Id, String Email, String Name)
        {
            this.Id = Id;
            this.Email = Email;
            this.Name = Name;
        }

        public static User fromJSON(JObject jObjectUser)
        {            
            User user = new User();
            user.Id = (String)jObjectUser["_id"];
            user.Name = (String)jObjectUser["name"];
            user.Email = (String)jObjectUser["email"];

            return user;
        }

        public async static Task<String> Login(String json)
        {
            String responseContent = await Request.Post("login", json);
            return responseContent;
        }

        public static User parseUser(String jsonResult)
        {
            Object obj = JsonConvert.DeserializeObject(jsonResult);
            JContainer userContainer = (JContainer)((JObject)(obj));

            User user = new User();
            user.Id = (String)userContainer["_id"];
            user.Email = (String)userContainer["email"];
            user.Name = (String)userContainer["name"];
            user.FirstName = (String)userContainer["first_name"];
            user.LastName = (String)userContainer["last_name"];
            user.Facebook = Int32.Parse((String)userContainer["fb"]);

            user.Avatar = new BitmapImage(new Uri((String)userContainer["avatar"], UriKind.Absolute));
            return user;
        }
    }
}
