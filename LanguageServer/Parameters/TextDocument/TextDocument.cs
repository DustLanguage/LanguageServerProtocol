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

    public string GetWordAtPosition(Position position)
    {
      int index = Text.GetPosition(position);

      int start = Text.Substring(0, index).LastIndexOfAny(new [] { ' ', '\n' }) + 1;
      
      if (start == -1)
      {
        start = 0;
      }
      
      int end = Text.IndexOfAny(new[] {' ', '(', '\n'}, start);
      
      if (end == -1)
      {
        end = Text.Length;
      }
      
      
      return Text.Substring(start, end - start);
    }
  }
}