namespace LanguageServer.Parameters.TextDocument
{
  public class TextDocumentPositionParams
  {
    public TextDocumentIdentifier TextDocument { get; set; }

    public Position Position { get; set; }
  }
}