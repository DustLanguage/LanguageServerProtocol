namespace LanguageServer.Parameters
{
  public class VersionedTextDocumentIdentifier : TextDocumentIdentifier
  {
    public long Version { get; set; }
  }
}