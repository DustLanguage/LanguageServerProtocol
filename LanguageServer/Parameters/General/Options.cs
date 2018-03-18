namespace LanguageServer.Parameters.General
{
  public class TextDocumentSyncOptions
  {
    public bool? OpenClose { get; set; }
    public TextDocumentSyncKind? Change { get; set; }
    public bool? WillSave { get; set; }
    public bool? WillSaveWaitUntil { get; set; }
    public SaveOptions Save { get; set; }
  }

  public class SaveOptions
  {
    public bool? IncludeText { get; set; }
  }

  public class CompletionOptions
  {
    public bool? ResolveProvider { get; set; }

    public string[] TriggerCharacters { get; set; }
  }

  public class SignatureHelpOptions
  {
    public string[] TriggerCharacters { get; set; }
  }

  public class CodeLensOptions
  {
    public bool? ResolveProvider { get; set; }
  }

  public class DocumentOnTypeFormattingOptions
  {
    public string FirstTriggerCharacter { get; set; }

    public string[] MoreTriggerCharacter { get; set; }
  }

  public class DocumentLinkOptions
  {
    public bool? ResolveProvider { get; set; }
  }

  public class ExecuteCommandOptions
  {
    public string[] Commands { get; set; }
  }
}