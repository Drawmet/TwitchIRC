// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Program
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using TwitchIRC.Core;
using TwitchIRC.Net;
using TwitchIRC.Utils;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WMPLib;
using Newtonsoft.Json;
using System.IO;

namespace TwitchIRC
{
  internal class Program
  {
        private static void Main(string[] args)
        {
            string ShortUrl;
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() != DialogResult.OK)
                return;
            Client client = new Client(new BoxClient(), new BitLy(), new Tinycc());
            client = JsonConvert.DeserializeObject<Client>(File.ReadAllText("accounts.json"));
            client.Init(loginForm.BoxLogin.Text,loginForm.BoxPassword.Text,loginForm.BoxFileId.Text,loginForm.ChooseShortener);
            ShortUrl = client.GetLink();
            File.WriteAllText("accounts.json", JsonConvert.SerializeObject(client));
            TwitchClient twclient = new TwitchClient();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            Console.WriteLine("Switch Ip and press Enter");
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = "rabota.mp3";
            player.controls.play();
            Console.Read();
            player.controls.stop();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            Console.WriteLine("Switch Ip and press Enter");
            player.URL = "rabota.mp3";
            player.controls.play();
            Console.Read();
            player.controls.stop();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            Console.WriteLine("Switch Ip and press Enter");
            player.URL = "rabota.mp3";
            player.controls.play();
            Console.Read();
            player.controls.stop();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            Console.WriteLine("Switch Ip and press Enter");
            player.URL = "rabota.mp3";
            player.controls.play();
            Console.Read();
            player.controls.stop();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            Console.WriteLine("Switch Ip and press Enter");
            player.URL = "rabota.mp3";
            player.controls.play();
            Console.Read();
            player.controls.stop();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            twclient.Registration(loginForm);
            twclient.GetText(ShortUrl);
            twclient.Spam();
            player.URL = "rabota.mp3";
            player.controls.play();
        }
  }
}
