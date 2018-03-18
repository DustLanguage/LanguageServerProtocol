namespace LanguageServer.Parameters.TextDocument
{
  public class DidSaveTextDocumentParams
  {
    public TextDocumentIdentifier TextDocument { get; set; }

    public string Text { get; set; }
  }
}