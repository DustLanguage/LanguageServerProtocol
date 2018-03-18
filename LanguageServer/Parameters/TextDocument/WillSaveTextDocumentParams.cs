namespace LanguageServer.Parameters.TextDocument
{
  public class WillSaveTextDocumentParams
  {
    public TextDocumentIdentifier TextDocument { get; set; }

    public TextDocumentSaveReason Reason { get; set; }
  }
}