namespace LanguageServer.Parameters.TextDocument
{
  public class DocumentOnTypeFormattingParams
  {
    public TextDocumentIdentifier TextDocument { get; set; }

    public Position Position { get; set; }

    public string Ch { get; set; }

    public FormattingOptions Options { get; set; }
  }
}