using System;

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
      string[] lines = Text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
      int textPosition = position.Character;

      for (int i = 0; i < position.Line; i++)
      {
        textPosition += lines[i].Length + Environment.NewLine.Length;
      }

      return textPosition;   
    }
  }
}