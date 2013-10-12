using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnforceWindowsPhoneApp.Utils
{
    class Comment
    {
        public String Id { get; set; }
        public User Author { get; set; }
        public String Text { get; set; }

        public DateTime Date { get; set; }

        public int Like { get; set; }
        public int Dislike { get; set; }

        private static Comment comment = null;

        public static List<Comment> parseList(String jsonResult)
        {
            Object obj = JsonConvert.DeserializeObject(jsonResult);
            JContainer jContainer = (JContainer)(((JArray)(obj)));

            List<Comment> comments = new List<Comment>();
            for (int i = 0; i < jContainer.Count; i++)
            {
                Comment comment = new Comment();
                comment = fromJSON((JObject)jContainer[i]);
                comments.Add(comment);
            }
            return comments;
        }

        public static Comment fromJSON(JObject jObjectComplaint)
        {
            comment = new Comment();

            comment.Id = (String)jObjectComplaint["_id"];
            comment.Text = (String)jObjectComplaint["text"];
            comment.Like = Int32.Parse((String)jObjectComplaint["like"]);
            comment.Dislike = Int32.Parse((String)jObjectComplaint["dislike"]);
            comment.Date = Convert.ToDateTime((String)jObjectComplaint["date"]);

            User author = new User();
            Newtonsoft.Json.Linq.JObject jObjectUser = (Newtonsoft.Json.Linq.JObject)jObjectComplaint["author"];
            author.Id = (String)jObjectUser["_id"];
            author.Name = (String)jObjectUser["name"];
            author.Email = (String)jObjectUser["email"];

            author.Avatar = new BitmapImage(new Uri((String)jObjectUser["avatar"], UriKind.Absolute));
            comment.Author = author;

            return comment;
        }

        public async static Task<String> GetComments(String url)
        {
            String responseContent = await Request.Get(url);
            return responseContent;
        }

    }
}
