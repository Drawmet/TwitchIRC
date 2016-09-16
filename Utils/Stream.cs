// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Utils.Stream
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using Newtonsoft.Json;

namespace TwitchIRC.Utils
{
  internal class Stream
  {
    [JsonProperty("_id")]
    public long Id { get; set; }

    [JsonProperty("game")]
    public string Game { get; set; }

    [JsonProperty("viewers")]
    public int Viewers { get; set; }

    [JsonProperty("is_playlist")]
    public bool IsPlaylist { get; set; }

    [JsonProperty("channel")]
    public Channel Channel { get; set; }
  }
}
