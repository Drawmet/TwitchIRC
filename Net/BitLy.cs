using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Text;
using System.Threading;

namespace TwitchIRC.Net
{
    public class BitLy
    {
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        string access_token { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        string apiKey { get; set; }
        Http http { get; set; }
        public string ShortUrl { get; set; }

        public void Auth()
        {
            http = new Http();
            this.Client_id = "b88d7fa211326bc278a070c74452e064c2911eba";
            this.Client_secret = "50ab09eca99926f84c2472c7fac123ba1c52cdfa";
            this.apiKey = "R_17f0221381b94dd4a9cf8c4e305fb2f8";
            this.password = password;
            this.access_token = http.SendPostRequest("https://api-ssl.bitly.com/oauth/access_token", String.Format("client_id={0}&client_secret={1}", Client_id, Client_secret), "", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(login + ":" + password)));
        }
        public string GetShortUrl (string url)
        {
            dynamic staff = JsonConvert.DeserializeObject(http.GetRequest(String.Format("https://api-ssl.bitly.com/v3/shorten?access_token={0}&longUrl={1}", access_token, url)));
            this.ShortUrl = "bit.ly/" + staff.data.hash;
            return ShortUrl; 
        }
    }
}
