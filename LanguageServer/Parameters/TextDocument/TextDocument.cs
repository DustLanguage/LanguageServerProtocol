using System;
using LanguageServer.Extensions;

namespace LanguageServer.Parameters.TextDocument
{
  public class TextDocument
  {
    public Uri Uri { get; set; }

    public string LanguageId { get; set; }

    public long Version { get; set; }

    public string Text { get; set; }

    public int GetPosition(Position position)
    {
      return Text.GetPosition(position);
    }
  }
}