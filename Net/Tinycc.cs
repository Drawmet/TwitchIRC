using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitchIRC.Net
{
    public class Tinycc
    {
        public string Login { get; set; }
        public string Password { get; set; }
        string ApiKey { get; set; }
        public string ShortUrl { get; set; }
        Http http = new Http();
        string GetApi()
        {
            string Auth = http.SendPostRequest("http://tiny.cc/ajax/login", String.Format("username={0}&password={1}",Login,Password), "");
            dynamic staff = JsonConvert.DeserializeObject( http.GetRequest("http://tiny.cc/api/generate_key"));
            this.ApiKey = staff.api_key;
            return ApiKey;
        }
        public string GetShortUrl(string LongUrl)
        {
            dynamic staff = JsonConvert.DeserializeObject( http.GetRequest(String.Format("http://tiny.cc/?c=rest_api&m=shorten&version=2.0.3&format=json&longUrl={0}&login={1}&apiKey={2}", LongUrl, Login, GetApi())));
            ShortUrl = "tiny.cc/";
            ShortUrl += staff.results.hash;
            return ShortUrl;
        }
    }
}
