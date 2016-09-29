// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Net.Http
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TwitchIRC.Net
{
  public class Http
  {
    public RCookie Cookies = new RCookie();
    public string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36";

    public Image GetImageFromUrl(string url)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
      if (this.Cookies != null && this.Cookies.CookiesContainer != null)
        httpWebRequest.CookieContainer = this.Cookies.CookiesContainer;
      using (HttpWebResponse httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse())
      {
        using (Stream responseStream = httpWebResponse.GetResponseStream())
        {
          if (responseStream != null)
            return Image.FromStream(responseStream);
        }
      }
      return (Image) null;
    }

    public byte[] GetRequestByte(string url)
    {
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Method = "GET";
        httpWebRequest.Accept = "application/json";
        httpWebRequest.UserAgent = this.UserAgent;
        httpWebRequest.Timeout = 20000;
        if (this.Cookies != null && this.Cookies.CookiesContainer != null)
          httpWebRequest.CookieContainer = this.Cookies.CookiesContainer;
        HttpWebResponse httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        if (httpWebResponse.StatusCode == HttpStatusCode.OK)
        {
          Stream responseStream = httpWebResponse.GetResponseStream();
          if (responseStream == null)
            return (byte[]) null;
          return new BinaryReader(responseStream).ReadBytes(500000);
        }
        httpWebResponse.Close();
      }
      catch (Exception ex)
      {
        return (byte[]) null;
      }
      return (byte[]) null;
    }

    public string GetRequest(string url)
    {
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Method = "GET";
        httpWebRequest.Accept = "application/json";
        httpWebRequest.UserAgent = this.UserAgent;
        httpWebRequest.Timeout = 20000;
        if (this.Cookies != null && this.Cookies.CookiesContainer != null)
          httpWebRequest.CookieContainer = this.Cookies.CookiesContainer;
        string str = string.Empty;
        HttpWebResponse httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        if (httpWebResponse.StatusCode == HttpStatusCode.OK)
        {
          str = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
          if (this.Cookies != null && this.Cookies.CookiesContainer != null)
          {
            foreach (Cookie cookie in httpWebResponse.Cookies)
              this.Cookies.Set(cookie);
          }
        }
        httpWebResponse.Close();
        return str;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    public string SendPostRequest(string url, string req, string referer, bool local = false)
    {
      try
      {
        byte[] bytes = Encoding.UTF8.GetBytes(req);
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.CookieContainer = this.Cookies.CookiesContainer ?? new CookieContainer();
        httpWebRequest.UserAgent = this.UserAgent;
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        httpWebRequest.Method = "POST";
        if (referer != "")
          httpWebRequest.Referer = referer;
        httpWebRequest.ContentLength = (long) bytes.Length;
        httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
        HttpWebResponse httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
        string str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd();
        foreach (Cookie cookie in httpWebResponse.Cookies)
          this.Cookies.Set(cookie);
        if (local)
          str = httpWebResponse.ResponseUri.ToString();
        httpWebResponse.Close();
        streamReader.Close();
        return str;
      }
      catch (WebException ex)
      {
        string str = string.Empty;
        if (ex.Status != WebExceptionStatus.ProtocolError)
          return str;
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          str = streamReader.ReadToEnd();
        return str;
      }
    }
        public string SendPostRequest(string url, string req, string referer, string headers, bool local = false)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(req);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.CookieContainer = this.Cookies.CookiesContainer ?? new CookieContainer();
                httpWebRequest.UserAgent = this.UserAgent;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";
                if (referer != "")
                    httpWebRequest.Referer = referer;
                httpWebRequest.Headers.Add("Authorization", headers);
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
                string str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd();
                foreach (Cookie cookie in httpWebResponse.Cookies)
                    this.Cookies.Set(cookie);
                if (local)
                    str = httpWebResponse.ResponseUri.ToString();
                httpWebResponse.Close();
                streamReader.Close();
                return str;
            }
            catch (WebException ex)
            {
                string str = string.Empty;
                if (ex.Status != WebExceptionStatus.ProtocolError)
                    return str;
                using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
                    str = streamReader.ReadToEnd();
                return str;
            }
        }

        public async Task<string> GetRequestAsync(string url)
    {
      string str;
      try
      {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
        request.Method = "GET";
        request.UserAgent = this.UserAgent;
        request.Timeout = 20000;
        if (this.Cookies != null && this.Cookies.CookiesContainer != null)
          request.CookieContainer = this.Cookies.CookiesContainer;
        string content = string.Empty;
        HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
        if (response.StatusCode == HttpStatusCode.OK)
        {
          content = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
          if (this.Cookies != null && this.Cookies.CookiesContainer != null)
          {
            foreach (Cookie cookie in response.Cookies)
              this.Cookies.Set(cookie);
          }
        }
        response.Close();
        str = content;
      }
      catch (Exception ex)
      {
        str = string.Empty;
      }
      return str;
    }

    public async Task<string> SendPostRequestAsync(string url, string req, string referer)
    {
      string str1;
      try
      {
        byte[] requestData = Encoding.UTF8.GetBytes(req);
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
        request.CookieContainer = this.Cookies.CookiesContainer ?? new CookieContainer();
        request.UserAgent = this.UserAgent;
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";
        if (!string.IsNullOrWhiteSpace(referer))
          request.Referer = referer;
        request.ContentLength = (long) requestData.Length;
        request.GetRequestStream().Write(requestData, 0, requestData.Length);
        HttpWebResponse resp = (HttpWebResponse) await request.GetResponseAsync();
        string content = await new StreamReader(request.GetResponse().GetResponseStream()).ReadToEndAsync();
        foreach (Cookie cookie in resp.Cookies)
          this.Cookies.Set(cookie);
        resp.Close();
        str1 = content;
      }
      catch (WebException ex)
      {
        string str2 = string.Empty;
        if (ex.Status != WebExceptionStatus.ProtocolError)
        {
          str1 = str2;
        }
        else
        {
          using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
            str2 = streamReader.ReadToEnd();
          str1 = str2;
        }
      }
      return str1;
    }
  }
}
