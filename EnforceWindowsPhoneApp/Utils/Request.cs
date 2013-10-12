using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EnforceWindowsPhoneApp.Utils
{
    class Request
    {
        private static String domain = "http://devel.enforceapp.com/";
        private static HttpClient client = new HttpClient();                


        /*public void MakeRequest(String param, String json)
        {
            this.json = json;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + param);
            webRequest.Method = "POST";
            webRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), webRequest);
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream postStream = webRequest.EndGetRequestStream(asynchronousResult);

            //String json = @"{""email"":""onur@onur.com"",""password"":""123""}";
            //String json = "{\"email\" : \"" + email + "\", \"password\" : \"" + password + "\"}";

            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();
            webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);
        }


        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                Stream streamResponse = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamResponse);

                response = streamReader.ReadToEnd();
                streamResponse.Close();
                streamReader.Close();
                webResponse.Close();
            }
            catch (Exception e) { }
        }*/


        public async static Task<String> Get(String url)
        {
            String responseContent = "";
            try
            {
                responseContent = await client.GetStringAsync(domain + url);
            }
            catch (Exception e) { }

            return responseContent;
        }

        public async static Task<String> Post(String url, String json)
        {
            String responseContent = "";
            try
            {
                StringContent postContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync((domain + url), postContent);
                if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    responseContent = "";
                }
                else
                {
                    responseContent = await res.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e) { }

            return responseContent;
        }

        public async static Task<String> GoogleGeoCodePost(double latitude, double longitude)
        {
            String responseContent = "";
            try
            {
                HttpClient googleClient = new HttpClient();
                HttpResponseMessage res = await client.GetAsync("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&sensor=true");
                if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    responseContent = "";
                }
                else
                {
                    responseContent = await res.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e) { }

            return responseContent;
        }
    }
}
