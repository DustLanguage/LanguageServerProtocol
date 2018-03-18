namespace LanguageServer.Parameters.TextDocument
{
  public class SignatureHelpRegistrationOptions : TextDocumentRegistrationOptions
  {
    public string[] TriggerCharacters { get; set; }
  }
}