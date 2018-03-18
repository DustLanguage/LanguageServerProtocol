using System;
using LanguageServer.Infrastructure.JsonDotNet;

namespace LanguageServer.Json
{
  public abstract class Serializer
  {
    public static Serializer Instance { get; set; } = new JsonDotNetSerializer();
    public abstract object Deserialize(Type objectType, string json);

    public abstract string Serialize(Type objectType, object value);
  }
}