using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchIRC.Core;
using TwitchIRC.Utils;
using WMPLib;

namespace TwitchIRC.Net
{
    public class TwitchClient
    {
        public string username;
        public string password;
        public string str;
        public string email;
        public string oauth;
        public TempMail UserTemp;
        public TempMailGu User;
        string letters;
        public string spamtext;
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public void Registration(LoginForm loginForm)
        {
            Http http = new Http();
            TimeSpan timeout = TimeSpan.FromSeconds(360);
            switch (loginForm.ChooseMail)
            {
                case 0:
                    this.User = new TempMailGu();
                    User.SetUser(http);
                    this.username = User.User;
                    this.email = User.Mail;
                    break;
                case 1:
                    this.UserTemp = new TempMail();
                    UserTemp.GetDomains();
                    UserTemp.GetNewMail();
                    this.username = UserTemp.MailName;
                    this.email = UserTemp.Mail;
                    break;
            }
            int day = 28;
            int month = 12;
            int year = 20;
            this.password = username;
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            str = loginForm.Username;
            driver.Navigate().GoToUrl("https://www.twitch.tv/signup");
            IWebElement element = driver.FindElementById("username");
            element.SendKeys(username);
            element = driver.FindElementById("password");
            element.SendKeys(password);
            element = driver.FindElementByName("email");
            element.SendKeys(email);
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
            player.URL = "rabota.mp3";
            player.controls.play();
            element = driver.FindElementByTagName("iFrame");
            driver.SwitchTo().Frame(element);
            element = driver.FindElementById("recaptcha-anchor"); ;
            string StateCAPTCHA = element.GetAttribute("aria-checked");
            while (!Convert.ToBoolean(StateCAPTCHA)) { StateCAPTCHA = element.GetAttribute("aria-checked"); }
            driver.SwitchTo().DefaultContent();
            player.controls.stop();
            driver.FindElement(By.ClassName("buttons")).Click();
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("user_display_name")));
//            driver.Navigate().GoToUrl("http://twitchapps.com/tmi/");
//            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("a")));
//            driver.FindElements(By.TagName("a"))[3].Click();
            driver.Navigate().GoToUrl("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=ksrhpqfqz7o6o1p6uafhili5ltw433m&redirect_uri=https://twitchapps.com/tmi/&scope=chat_login");
            driver.FindElement(By.TagName("button")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("tmiPasswordField")));
            element = driver.FindElementById("tmiPasswordField");
            oauth = element.GetAttribute("value");
            switch (loginForm.ChooseMail)
            {
                case 0:
                    letters = GetMail(http);
                    break;
                case 1:
                    letters = GetMail();
                    break;
            }
            string html = "twitch.tv/user";
            int min = 0;
            int max = 0;
            while (min < letters.Length)
            {
                string text = "";
                if (letters[min] == html[0])
                {

                    for (int m = min; m < (min + 14); m++)
                    {
                        text = text + letters[m];
                    }

                    if (html == text)
                    {
                        max = min++;
                        while (letters[max] != ' ')
                        {
                            max++;
                        }
                        html = "";
                        switch (loginForm.ChooseMail)
                        {
                            case 0: max -= 3; break;
                            case 1: max -= 2; break;
                        }
                        // max-2 to TempMail, and max - 3 fo TempMailGu
                        for (int index = min - 1; index < max; index++)
                        {
                            html = html + letters[index];
                        }
                        break;
                    }
                }
                min++;
            }
            driver.Navigate().GoToUrl("https://" + html);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("title")));
            driver.Quit();
        }
        string GetMail(Http http)
        {
            string letterhtml;
            User.GetLetter(http);
            letterhtml = User.MailText;
            return letterhtml;
        }
        string GetMail()
        {
            TwitchIRC.Letter[] letters = UserTemp.GetNewLetters();
            string letterhtml = letters[letters.Length - 1].MailText;
            return letterhtml;
        }
        public void Spam()
        {
            IRC irc = new IRC(oauth, username);
            Http http = new Http();
            List<string> list = new List<string>();
            int indfirst = 0;
            if (str != null)
            {
                string[] strArray = str.Split('-');
                int num1 = int.Parse(strArray[0]);
                indfirst = num1;
                int num2 = int.Parse(strArray[1]);
                while (num2 != 0)
                {
                    int num3 = num2 > 100 ? 100 : num2;
                    string request = http.GetRequest("https://api.twitch.tv/kraken/streams?limit=" + num3 + "&offset=" + num1 + "&client_id=ksrhpqfqz7o6o1p6uafhili5ltw433m");
                    num1 += num2;
                    num2 -= num3;
                    TwitchList result;
                    if (!JsonHelper.TryDeserializeObject<TwitchList>(request, out result))
                        return;

                    for (int index = 0; index < result.Streams.Length; ++index)
                        list.Add(result.Streams[index].Channel.Name);
                }
            }
            Console.WriteLine("Added {0} channels!\nSleep 31s. please wait...", (object)list.Count);
            Thread.Sleep(31000);
            Console.WriteLine("Start Work!");
            for (int index = indfirst; index < list.Count; ++index)
            {
                irc.SendMessage(string.Format("PRIVMSG #{0} :{1}", (object)list[index], (object)this.spamtext));
                if ((double)(index + 1) * 1.0 / 20.0 % 1.0 == 0.0 && index != 0)
                {
                    Console.WriteLine("20 chats have been spamed!");
                    Thread.Sleep(31000);
                }
            }
            Console.WriteLine("End Work.");
        }
        public void GetText(string Link)
        {
            string[] readText = File.ReadAllLines("rabotaText.txt");
            if (File.Exists("WorkText.txt"))
            {
                string[] read = File.ReadAllLines("WorkText.txt");
                if (read[0] == "") { File.WriteAllLines("WorkText.txt", readText); }
                else
                {
                    readText = File.ReadAllLines("WorkText.txt"); 
                }
            }
            else
            {
                File.WriteAllLines("WorkText.txt", readText);
            }
            spamtext = readText[0];
            string link = "@l";
            int min = 0;
            while (min < spamtext.Length)
            {
                string text = "";
                if (spamtext[min] == link[0])
                {
                    text += "@" + spamtext[min + 1];
                    if (link == text)
                    {
                        link = "";
                        for (int ind = 0; ind < min; ind++)
                            link += spamtext[ind];
                        link += Link;
                        for (int ind = min + 6; ind < spamtext.Length; ind++)
                            link += spamtext[ind];
                        spamtext = "";
                        spamtext = link;
                        break;
                    }
                }
                min++;
            }
            for (int ind = 1; ind < readText.Length; ind++)
                readText[ind - 1] = readText[ind];
            Array.Clear(readText, readText.Length - 1, 1);
            File.WriteAllLines("WorkText.txt", readText);
        }
    }
}
