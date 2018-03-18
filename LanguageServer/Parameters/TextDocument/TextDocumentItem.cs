using System;

namespace LanguageServer.Parameters.TextDocument
{
  public class TextDocumentItem
  {
    public Uri Uri { get; set; }

    public string LanguageId { get; set; }

    public long Version { get; set; }

    public string Text { get; set; }
  }
}