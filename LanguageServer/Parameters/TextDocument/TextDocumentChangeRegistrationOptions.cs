namespace LanguageServer.Parameters.TextDocument
{
  public class TextDocumentChangeRegistrationOptions : TextDocumentRegistrationOptions
  {
    public TextDocumentSyncKind SyncKind { get; set; }
  }
}