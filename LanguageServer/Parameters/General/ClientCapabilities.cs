namespace LanguageServer.Parameters.General
{
  public class ClientCapabilities
  {
    public WorkspaceClientCapabilities Workspace { get; set; }

    public TextDocumentClientCapabilities TextDocument { get; set; }

    public dynamic Experimental { get; set; }
  }

  public class WorkspaceClientCapabilities
  {
    public bool? ApplyEdit { get; set; }

    public EditCapabilities WorkspaceEdit { get; set; }

    public RegistrationCapabilities DidChangeConfiguration { get; set; }

    public RegistrationCapabilities DidChangeWatchedFiles { get; set; }

    public RegistrationCapabilities Symbol { get; set; }

    public RegistrationCapabilities ExecuteCommand { get; set; }
  }

  public class EditCapabilities
  {
    public bool? DocumentChanges { get; set; }
  }

  public class RegistrationCapabilities
  {
    public bool? DynamicRegistration { get; set; }
  }

  public class TextDocumentClientCapabilities
  {
    public SynchronizationCapabilities Synchronization { get; set; }

    public CompletionCapabilities Completion { get; set; }

    public RegistrationCapabilities Hover { get; set; }

    public RegistrationCapabilities SignatureHelp { get; set; }

    public RegistrationCapabilities References { get; set; }

    public RegistrationCapabilities DocumentHighlight { get; set; }

    public RegistrationCapabilities DocumentSymbol { get; set; }

    public RegistrationCapabilities Formatting { get; set; }

    public RegistrationCapabilities RangeFormatting { get; set; }

    public RegistrationCapabilities OnTypeFormatting { get; set; }

    public RegistrationCapabilities Definition { get; set; }

    public RegistrationCapabilities CodeAction { get; set; }

    public RegistrationCapabilities CodeLens { get; set; }

    public RegistrationCapabilities DocumentLink { get; set; }

    public RegistrationCapabilities Rename { get; set; }
  }

  public class SynchronizationCapabilities : RegistrationCapabilities
  {
    public bool? WillSave { get; set; }

    public bool? WillSaveWaitUntil { get; set; }

    public bool? DidSave { get; set; }
  }

  public class CompletionCapabilities : RegistrationCapabilities
  {
    public CompletionItemCapabilities CompletionItem { get; set; }
  }

  public class CompletionItemCapabilities
  {
    public bool? SnippetSupport { get; set; }
  }
}