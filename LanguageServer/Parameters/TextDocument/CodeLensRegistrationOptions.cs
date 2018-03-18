namespace LanguageServer.Parameters.TextDocument
{
  public class CodeLensRegistrationOptions : TextDocumentRegistrationOptions
  {
    public bool? ResolveProvider { get; set; }
  }
}