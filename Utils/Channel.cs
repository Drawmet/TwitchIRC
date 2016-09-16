// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Utils.Channel
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using Newtonsoft.Json;

namespace TwitchIRC.Utils
{
  internal class Channel
  {
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("broadcaster_language")]
    public string BroadcasterLanguage { get; set; }

    [JsonProperty("game")]
    public string Game { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
  }
}
