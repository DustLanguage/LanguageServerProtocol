using System;

namespace LanguageServer.Parameters.TextDocument
{
  public class PublishDiagnosticsParams
  {
    public Uri Uri { get; set; }

    public Diagnostic[] Diagnostics { get; set; }
  }
}