// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Utils.JsonHelper
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using Newtonsoft.Json;
using System;

namespace TwitchIRC.Utils
{
  public static class JsonHelper
  {
    public static bool TryDeserializeObject<T>(string json, out T result)
    {
      result = default (T);
      try
      {
        result = JsonConvert.DeserializeObject<T>(json);
      }
      catch (Exception ex)
      {
        return false;
      }
      return (object) result != null;
    }
  }
}
