using LanguageServer.Json;

namespace LanguageServer.Parameters.General
{
  public class ServerCapabilities
  {
    public NumberOrObject<TextDocumentSyncKind, TextDocumentSyncOptions> TextDocumentSync { get; set; }

    public bool? HoverProvider { get; set; }

    public CompletionOptions CompletionProvider { get; set; }

    public SignatureHelpOptions SignatureHelpProvider { get; set; }

    public bool? DefinitionProvider { get; set; }

    public bool? ReferencesProvider { get; set; }

    public bool? DocumentHighlightProvider { get; set; }

    public bool? DocumentSymbolProvider { get; set; }

    public bool? WorkspaceSymbolProvider { get; set; }

    public bool? CodeActionProvider { get; set; }

    public CodeLensOptions CodeLensProvider { get; set; }

    public bool? DocumentFormattingProvider { get; set; }

    public bool? DocumentRangeFormattingProvider { get; set; }

    public DocumentOnTypeFormattingOptions DocumentOnTypeFormattingProvider { get; set; }

    public bool? RenameProvider { get; set; }

    public DocumentLinkOptions DocumentLinkProvider { get; set; }

    public ExecuteCommandOptions ExecuteCommandProvider { get; set; }

    public dynamic Experimental { get; set; }
  }
}