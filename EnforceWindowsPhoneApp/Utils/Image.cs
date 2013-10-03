using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EnforceWindowsPhoneApp.Utils
{
    class Image
    {

        public async static Task<BitmapImage> DownloadImage(String url)
        {
            BitmapImage bmp = new BitmapImage(new Uri("http://enforceapp.com" + url, UriKind.Absolute));
            return bmp;
        }

    }
}
