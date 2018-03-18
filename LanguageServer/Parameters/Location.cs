using System;

namespace LanguageServer.Parameters
{
  public class Location
  {
    public Uri Uri { get; set; }
    public Range Range { get; set; }
  }
}