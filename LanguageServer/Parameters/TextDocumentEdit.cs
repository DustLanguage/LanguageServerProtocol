namespace LanguageServer.Parameters
{
  public class TextDocumentEdit
  {
    public VersionedTextDocumentIdentifier TextDocument { get; set; }

    public TextEdit[] Edits { get; set; }
  }
}