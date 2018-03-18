using LanguageServer.Json;

namespace LanguageServer.Parameters.TextDocument
{
  public class Hover
  {
    public ArrayOrObject<StringOrObject<MarkedString>, StringOrObject<MarkedString>> Contents { get; set; }

    public Range Range { get; set; }
  }
}