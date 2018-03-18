namespace LanguageServer.Parameters.TextDocument
{
  public class CompletionItem
  {
    public string Label { get; set; }

    public CompletionItemKind? Kind { get; set; }

    public string Detail { get; set; }

    public string Documentation { get; set; }

    public string SortText { get; set; }

    public string FilterText { get; set; }

    public string InsertText { get; set; }

    public InsertTextFormat? InsertTextFormat { get; set; }

    public TextEdit TextEdit { get; set; }

    public TextEdit[] AdditionalTextEdits { get; set; }

    public Command Command { get; set; }

    public dynamic Data { get; set; }
  }
}