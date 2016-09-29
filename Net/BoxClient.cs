using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchIRC.Net;

namespace TwitchIRC.Net
{
    public class BoxClient
    {
        public string client_id { get; set; }
        public string secret_id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string UrlDownload { get; set; }
        public string File_Id { get; set; }
        string code { get; set; }
        Http http { get; set; }

        public void Login(string login, string password)
        {
            this.login = login;
            this.password = password;
            Login();
        }

        public void Login()
        {
            this.client_id = "rygymkssca51qk2ugi22uaj5e98g6dve";
            this.secret_id = "wNchIao5NoENVOsm2TQprQnlOsLleGSc";
            
            string url = "https://account.box.com/api/oauth2/authorize?response_type=code&client_id=" + client_id + "&state=" + secret_id;
            this.http = new Http();
            TimeSpan timeout = TimeSpan.FromSeconds(60);
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("-incognito");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);
            IWebElement element = driver.FindElementById("login");
            element.SendKeys(login);
            element = driver.FindElementById("password");
            element.SendKeys(password);
            driver.FindElementByName("login_submit").Click();
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("consent_accept_button")));
            driver.FindElementById("consent_accept_button").Click();
            Thread.Sleep(1500);
            string urlpage = driver.Url;
            driver.Quit();
            string code = "code";
            int min = 0;
            while (min < urlpage.Length)
            {
                string text = "";
                if (urlpage[min] == code[0])
                {
                    for (int m = min; m < min + 4; m++)
                    {
                        text = text + urlpage[m];
                    }
                    if (code == text)
                    {
                        code = "";
                        // max-2 to TempMail, and max - 3 fo TempMailGu
                        for (int index = min + 5; index < urlpage.Length; index++)
                        {
                            code = code + urlpage[index];
                        }
                        break;
                    }
                }
                min++;
            }
            this.code = code;
        }
        public void TakeUrl(string FileId)
        {
            File_Id = FileId;
            TakeUrl();
        }
        public void TakeUrl()
        {
            TokenResponse Response = JsonConvert.DeserializeObject<TokenResponse>(http.SendPostRequest("https://api.box.com/oauth2/token", "grant_type=authorization_code&code=" + code + "&client_id=" + client_id + "&client_secret=" + secret_id, "https://api.box.com/v2"));
            this.UrlDownload = "https://api.box.com/2.0/files/" + File_Id + "/content?access_token=" + Response.AccessToken;
        }
    }
}
