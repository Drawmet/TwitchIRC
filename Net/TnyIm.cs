using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitchIRC.Net
{
    public class TnyIm
    {
        Http http = new Http();
        string ShortUrl { get; set; }
        public string GetShortUrl(string Link)
        {
            dynamic staff = JsonConvert.DeserializeObject(http.GetRequest(String.Format("http://tny.im/yourls-api.php?action=shorturl&url={0}&format=json", Link)));
            string TempStr = staff.message.shorturl;
            ShortUrl = "";
            for (int ind = 7; ind < TempStr.Length; ind++)
                ShortUrl += TempStr[ind];
            return ShortUrl;
        }
    }
}
