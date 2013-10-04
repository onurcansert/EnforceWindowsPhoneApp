using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
