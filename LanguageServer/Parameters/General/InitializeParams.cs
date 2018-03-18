using System;

namespace LanguageServer.Parameters.General
{
  public class InitializeParams
  {
    public int? ProcessId { get; set; }

    public Uri RootUri { get; set; }

    public dynamic InitializationOptions { get; set; }

    public ClientCapabilities Capabilities { get; set; }

    public string Trace { get; set; }
  }

  public class InitializeResult
  {
    public ServerCapabilities Capabilities { get; set; }
  }

  public class InitializeErrorData
  {
    public bool Retry { get; set; }
  }
}