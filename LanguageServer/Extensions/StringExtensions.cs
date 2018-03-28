using System;
using LanguageServer.Parameters;

namespace LanguageServer.Extensions
{
  public static class StringExtensions
  {
    public static int GetPosition(this string text, Position position)
    {
      string[] lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
      int textPosition = position.Character;

      for (int i = 0; i < position.Line; i++)
      {
        textPosition += lines[i].Length + Environment.NewLine.Length;
      }

      return textPosition;
    }
  }
}