using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace TwitchIRC.Net
{
    public class TempMailGu
    {
        string GUurl = "http://api.guerrillamail.com/ajax.php";
        public string User;
        string Data = "";
        public string Mail;
        public string MailId;
        const int BufferSize = 1024;
        byte[] RandomBuffer;
        int BufferOffset;
        string token;
        public string MailText;
        static System.Security.Cryptography.RNGCryptoServiceProvider rng;
        static TempMailGu() { rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); }
        public TempMailGu()
        {
            RandomBuffer = new byte[BufferSize];
            BufferOffset = RandomBuffer.Length;
        }
        private byte Next()
        {
            if (BufferOffset >= RandomBuffer.Length)
            {
                rng.GetBytes(RandomBuffer);
                BufferOffset = 0;
            }
            return RandomBuffer[BufferOffset++];
        }
        public int Next(int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            return minValue + Next() % range;
        }

        int MinStringLength = 8;
        int MaxStringLength = 13;
        public string NextString()
        {
            StringBuilder sb = new StringBuilder();
            int count = Next(MinStringLength, MaxStringLength);
            for (int i = 0; i < count; i++)
                sb.Append((char)Next('a', 'z'));
            return sb.ToString();
        }
        public void SetUser(Http httpreq)
        {
            User = NextString();
            Data = GUurl + "?f=set_email_user&email_user=" + User + "&site=guerrillamail.com&lang=en";
            dynamic stuff = JsonConvert.DeserializeObject(httpreq.GetRequest(Data));
            Mail = stuff.email_addr;
            token = stuff.sid_token;
        }

        public void GetLetter(Http httpreq)
        {
            Data = GUurl + "?f=check_email&seq=0";
            Letters letters = JsonConvert.DeserializeObject<Letters>(httpreq.GetRequest(Data));
            MailId = Convert.ToString(letters.List[0].MailId);
            Data = GUurl + "?f=fetch_email&email_id=" + MailId;
            Letter letter = JsonConvert.DeserializeObject<Letter>(httpreq.GetRequest(Data));
            MailText = letter.MailBody;
        }
    }
}