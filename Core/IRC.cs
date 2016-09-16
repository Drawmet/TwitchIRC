// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Core.IRC
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TwitchIRC.Core
{
  internal class IRC
  {
    public bool IsLogin;
    public List<string> Channels;
    public string Text;
    private StreamReader _reader;
    private StreamWriter _writer;

    public IRC(string oauth, string nick)
    {
      NetworkStream stream = new TcpClient("irc.chat.twitch.tv", 6667).GetStream();
      this._reader = new StreamReader((Stream) stream, Encoding.GetEncoding("iso8859-1"));
      this._writer = new StreamWriter((Stream) stream, Encoding.GetEncoding("iso8859-1"));
      new Thread(new ThreadStart(this.Listen)).Start();
      this.Login(oauth, nick);
    }

    public void Login(string oauth, string nick)
    {
      this.SendMessage("PASS " + oauth + "\r\nNICK " + nick);
    }

    public void Listen()
    {
      try
      {
        string str1;
        while ((str1 = this._reader.ReadLine()) != null)
        {
          string[] strArray1 = str1.Split(new char[1]
          {
            ' '
          }, 5);
          if (strArray1[0] == "PING")
          {
            this.SendMessage("PONG " + strArray1[1]);
            Console.WriteLine("PONG " + strArray1[1]);
          }
          string[] strArray2 = str1.Split(':');
          if (strArray2.Length > 1)
          {
            string str2 = "";
            if (strArray2.Length > 2)
            {
              for (int index = 2; index < strArray2.Length; ++index)
                str2 = str2 + strArray2[index] + "|";
            }
            if (!this.IsLogin)
            {
              this.IsLogin = !str2.Contains("Error logging in") && str2.Contains("Welcome");
              if (this.IsLogin)
                Console.WriteLine(str2);
            }
          }
        }
      }
      catch
      {
      }
    }

    public void SendMessage(string message)
    {
      this._writer.WriteLine(message);
      this._writer.Flush();
    }
  }
}
