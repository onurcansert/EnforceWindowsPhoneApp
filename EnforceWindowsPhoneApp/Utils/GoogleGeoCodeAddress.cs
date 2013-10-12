using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnforceWindowsPhoneApp.Utils
{
    class GoogleGeoCodeAddress
    {

        public async static Task<String> GetAddress(double latitude, double longitude)
        {
            String addressResponse = await Request.GoogleGeoCodePost(latitude, longitude);

            Object obj = JsonConvert.DeserializeObject(addressResponse);
            JContainer addressContainer = (Newtonsoft.Json.Linq.JContainer)(((Newtonsoft.Json.Linq.JObject)(obj)));
            String address = (String)(((Newtonsoft.Json.Linq.JArray)(addressContainer["results"]))[0])["formatted_address"];

            return address;
        }

    }
}