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

namespace TwitchIRC
{
  internal class Program
  {
        private static void Main(string[] args)
        {
            int day = 28;
            int month = 12;
            int year = 20;
            string username;
            string password;
            string oauth;
            string str;
            string html;
            Http http = new Http();
//            TempMail User = new TempMail();
            TempMailGu TempMail = new TempMailGu();
            TimeSpan timeout = TimeSpan.FromSeconds(60);
            TempMail.SetUser(http);
//            User.GetDomains();
//            User.GetNewMail();
            username = TempMail.User;
            password = username;
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() != DialogResult.OK)
                return;
            string spamText = loginForm.SpamText;
            str = loginForm.Username;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.twitch.tv/signup");
            IWebElement element = driver.FindElementById("username");
            element.SendKeys(username);
            element = driver.FindElementById("password");
            element.SendKeys(password);
            element = driver.FindElementByName("email");
            element.SendKeys(TempMail.Mail);
            Random random = new Random();
            month = random.Next(month) + 1;
            element = driver.FindElementByName("birthday.month");
            SelectElement mySelect = new SelectElement(element);
            mySelect.SelectByValue(Convert.ToString(month));
            element = driver.FindElementByName("birthday.day");
            mySelect = new SelectElement(element);
            day = random.Next(day) + 1;
            mySelect.SelectByValue(Convert.ToString(day));
            element = driver.FindElementByName("birthday.year");
            mySelect = new SelectElement(element);
            year = random.Next(year) + 1977;
            mySelect.SelectByValue(Convert.ToString(year));
            driver.FindElementById("recaptcha2-widget").Click();
            element = driver.FindElementByTagName("iFrame");
            driver.SwitchTo().Frame(element);
            element = driver.FindElementById("recaptcha-anchor");;
            string StateCAPTCHA = element.GetAttribute("aria-checked");
            while (!Convert.ToBoolean(StateCAPTCHA)) { StateCAPTCHA = element.GetAttribute("aria-checked"); }
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.ClassName("buttons")).Click();
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("user_display_name")));
            driver.Navigate().GoToUrl("http://twitchapps.com/tmi/");
            driver.FindElements(By.TagName("a"))[3].Click();
    //        driver.Navigate().GoToUrl("https://api.twitch.tv/kraken/oauth2/authorize?response_type=code&client_id=dx0o332v7owgr6d385qxu2qd5785jav&redirect_uri=http%3A%2F%2Flocalhost&scope=chat_login");
            driver.FindElement(By.TagName("button")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("tmiPasswordField")));
            element = driver.FindElementById("tmiPasswordField");
            oauth = element.GetAttribute("value");
            //            Letter[] letters = User.GetNewLetters();
            //            string letterhtml = letters[letters.Length - 1].MailText;
            TempMail.GetLetter(http);
            string letterhtml = TempMail.MailText;
            html = "twitch.tv/user";
            int min = 0;
            int max = 0;
            while (min < letterhtml.Length)
            {
                string text = "";
                if (letterhtml[min] == html[0])
                {

                    for (int m = min; m < (min + 14); m++)
                    {
                        text = text + letterhtml[m];
                    }

                    if (html == text)
                    {
                        max = min++;
                        while (letterhtml[max] != ' ')
                        {
                            max++;
                        }
                        html = "";
                        for (int index = min - 1; index < max - 3; index++)
                        {
                            html = html + letterhtml[index];
                        }
                        break;
                    }
                }
                min++;
            }
      driver.Navigate().GoToUrl("https://" + html);
      wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("title")));
      driver.Quit();
      IRC irc = new IRC(oauth, username);
      List<string> list = new List<string>();
      
      int indfirst=0;
      if (str != null)
      {
        string[] strArray = str.Split('-');
        int num1 = int.Parse(strArray[0]);
        indfirst = num1;
        int num2 = int.Parse(strArray[1]);
        while (num2 != 0)
        {
          int num3 = num2 > 100 ? 100 : num2;
          string request = http.GetRequest("https://api.twitch.tv/kraken/streams?limit=" + num3 + "&offset=" + num1 + "&client_id=dx0o332v7owgr6d385qxu2qd5785jav" );
          num1 += num2;
          num2 -= num3;
          TwitchList result;
          if (!JsonHelper.TryDeserializeObject<TwitchList>(request, out result))
                     return;
                   
          for (int index = 0; index < result.Streams.Length; ++index)
            list.Add(result.Streams[index].Channel.Name);
        }
      }
      Console.WriteLine("Added {0} channels!\nSleep 31s. please wait...", (object) list.Count);
      Thread.Sleep(31000);
      Console.WriteLine("Start Work!");
      for (int index = indfirst; index < list.Count; ++index)
      {
        irc.SendMessage(string.Format("PRIVMSG #{0} :{1}", (object) list[index], (object) spamText));
        if ((double) (index + 1) * 1.0 / 20.0 % 1.0 == 0.0 && index != 0)
        {
          Console.WriteLine("20 chats have been spamed!");
          Thread.Sleep(31000);
        }
      }
      Console.WriteLine("End Work.");
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = "rabota.mp3";
            player.controls.play();
            Console.ReadLine();
    }
  }
}
