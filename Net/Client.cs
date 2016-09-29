using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchIRC.Core;

namespace TwitchIRC.Net
{
    public class Client
    {
        public BoxClient box { get; set; }
        public BitLy ly { get; set; }
        public Tinycc tiny { get; set; }
        public IsGd gd { get; set; }
        public TnyIm tny { get; set; }
        public int ChooseShortener;
        string ShortUrl;
        public Client()
        {
            BoxClient box = new BoxClient();
            BitLy ly = new BitLy();
            Tinycc tiny = new Tinycc();
            IsGd gd = new IsGd();
            TnyIm tny = new TnyIm();
            this.gd = gd;
            this.box = box;
            this.ly = ly;
            this.tiny = tiny;
            this.tny = tny;
        }
        public void Init(string login, string password, string fileId, int ChooseShortener)
        {
            this.ChooseShortener = ChooseShortener;
            if (login != "" & password != "")
                box.Login(login, password);
            else
                box.Login(box.login, box.password);
            if (fileId != "")
                box.TakeUrl(fileId);
            else box.TakeUrl();
            ly.Auth();
        }

        public string GetLink()
        {
            switch (ChooseShortener)
            {
                case 0:
                    ShortUrl = tiny.GetShortUrl(box.UrlDownload);
                    ChooseShortener++;
                    Console.WriteLine(ShortUrl);
                    break;
                case 1:
                    ShortUrl = ly.GetShortUrl(box.UrlDownload);
                    ChooseShortener++;
                    Console.WriteLine(ShortUrl);
                    break;
                case 2:
                    ShortUrl = gd.GetShortUrl(box.UrlDownload);
                    ChooseShortener++;
                    Console.WriteLine(ShortUrl);
                    break;
                case 3:
                    ShortUrl = gd.GetShortUrl(box.UrlDownload);
                    ChooseShortener=0;
                    Console.WriteLine(ShortUrl);
                    break;
            }
            return ShortUrl;
        }
    }
}
