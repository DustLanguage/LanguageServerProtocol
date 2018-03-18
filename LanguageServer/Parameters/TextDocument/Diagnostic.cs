using LanguageServer.Json;

namespace LanguageServer.Parameters.TextDocument
{
  public class Diagnostic
  {
    public Range Range { get; set; }

    public DiagnosticSeverity? Severity { get; set; }

    public NumberOrString Code { get; set; }

    public string Source { get; set; }

    public string Message { get; set; }
  }
}