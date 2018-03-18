namespace LanguageServer.Parameters.TextDocument
{
  public class TextDocumentContentChangeEvent
  {
    public Range Range { get; set; }

    public long? RangeLength { get; set; }

    public string Text { get; set; }
  }
}