using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnforceWindowsPhoneApp.Utils
{
    class Comment
    {
        private String id;
        private User author;
        private String text;

        private DateTime date;

        private int like;
        private int dislike;

        public void setId(String id)
        {
            this.id = id;
        }

        public String getId()
        {
            return id;
        }

        public void setText(String text)
        {
            this.text = text;
        }

        public String getText()
        {
            return text;
        }

        public void setDate(DateTime date)
        {
            this.date = date;
        }

        public DateTime getDate()
        {
            return date;
        }

        public void setLikeCount(int like)
        {
            this.like = like;
        }

        public int getLikeCount()
        {
            return like;
        }

        public void setDislikeCount(int dislike)
        {
            this.dislike = dislike;
        }

        public int getDislikeCount()
        {
            return dislike;
        }

        public void setAuthor(User author)
        {
            this.author = author;
        }

        public User getAuthor()
        {
            return author;
        }
    }
}
