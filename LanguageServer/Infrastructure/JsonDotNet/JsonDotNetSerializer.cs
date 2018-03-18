﻿using System;
using LanguageServer.Json;
using Newtonsoft.Json;

namespace LanguageServer.Infrastructure.JsonDotNet
{
  public class JsonDotNetSerializer : Serializer
  {
    private readonly JsonSerializerSettings settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore,
      Converters = new JsonConverter[] {new EitherConverter()}
    };

    public override object Deserialize(Type objectType, string json)
    {
      return JsonConvert.DeserializeObject(json, objectType, settings);
    }

    public override string Serialize(Type objectType, object value)
    {
      return JsonConvert.SerializeObject(value, settings);
    }
  }
}