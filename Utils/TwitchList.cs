// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Utils.TwitchList
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using Newtonsoft.Json;

namespace TwitchIRC.Utils
{
  internal class TwitchList
  {
    [JsonProperty("streams")]
    public Stream[] Streams { get; set; }

    [JsonProperty("_total")]
    public int Total { get; set; }

    [JsonProperty("_links")]
    public Links Links { get; set; }
  }
}
