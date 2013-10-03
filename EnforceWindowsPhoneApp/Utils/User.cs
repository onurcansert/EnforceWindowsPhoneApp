using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnforceWindowsPhoneApp.Utils
{
    class User
    {
        public String Id { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }

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
    }
}
