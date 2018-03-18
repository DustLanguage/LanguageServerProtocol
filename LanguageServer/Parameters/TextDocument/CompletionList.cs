namespace LanguageServer.Parameters.TextDocument
{
  public class CompletionList
  {
    public bool IsIncomplete { get; set; }

    public CompletionItem[] Items { get; set; }
  }
}