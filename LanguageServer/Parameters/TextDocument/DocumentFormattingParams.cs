namespace LanguageServer.Parameters.TextDocument
{
  public class DocumentFormattingParams
  {
    public TextDocumentIdentifier TextDocument { get; set; }

    public FormattingOptions Options { get; set; }
  }
}