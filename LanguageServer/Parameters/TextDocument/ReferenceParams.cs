namespace LanguageServer.Parameters.TextDocument
{
  public class ReferenceParams : TextDocumentPositionParams
  {
    public ReferenceContext Context { get; set; }
  }
}