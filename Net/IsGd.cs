using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchIRC.Net
{
    public class IsGd
    {
        Http http = new Http();
        string ShortUrl { get; set; }
        public string GetShortUrl(string Link)
        {
            dynamic staff = http.GetRequest(String.Format("https://is.gd/create.php?format=json&url={0}", Link));
            string TempStr = staff.shorturl;
            ShortUrl = "";
            for (int ind = 8; ind < TempStr.Length; ind++)
                ShortUrl += TempStr[ind];
            return ShortUrl;
        }
    }
}
