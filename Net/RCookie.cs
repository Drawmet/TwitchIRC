using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitchIRC.Net
{
  public class RCookie
  {
    public string FilePath = "cookies.dat";

    public bool IsCookiesFound
    {
      get
      {
        return System.IO.File.Exists(this.FilePath);
      }
    }

    public CookieContainer CookiesContainer { get; set; }

    public RCookie()
    {
      this.Load();
    }

    [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

    public void Load()
    {
      this.CookiesContainer = new CookieContainer();
      if (!System.IO.File.Exists(this.FilePath))
        return;
      try
      {
        using (Stream serializationStream = (Stream) System.IO.File.Open(this.FilePath, FileMode.Open))
          this.CookiesContainer = (CookieContainer) new BinaryFormatter().Deserialize(serializationStream);
      }
      catch
      {
      }
    }

    public void Save(string text = "")
    {
      using (Stream serializationStream = (Stream) System.IO.File.Create(text + this.FilePath))
      {
        try
        {
          new BinaryFormatter().Serialize(serializationStream, (object) this.CookiesContainer);
        }
        catch
        {
        }
      }
    }

    public Cookie Get(string name, string uri)
    {
      CookieCollection cookies = this.CookiesContainer.GetCookies(new Uri(uri));
      for (int index = 0; index < cookies.Count; ++index)
      {
        if (cookies[index].Name.Contains(name))
          return cookies[index];
      }
      return (Cookie) null;
    }

    public void Set(Cookie cookie)
    {
      if (this.CookiesContainer == null)
        this.CookiesContainer = new CookieContainer();
      this.CookiesContainer.Add(cookie);
    }

    public void Delete(string name, string uri)
    {
      CookieCollection cookies = this.CookiesContainer.GetCookies(new Uri(uri));
      for (int index = 0; index < cookies.Count; ++index)
      {
        if (string.Equals(cookies[index].Name, name))
          cookies[index].Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1.0));
      }
    }
  }
}