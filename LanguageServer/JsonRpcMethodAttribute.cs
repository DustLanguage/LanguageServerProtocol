using System;

namespace LanguageServer
{
  [AttributeUsage(AttributeTargets.Method)]
  public class JsonRpcMethodAttribute : Attribute
  {
    public JsonRpcMethodAttribute(string method)
    {
      Method = method;
    }

    public string Method { get; }
  }
}