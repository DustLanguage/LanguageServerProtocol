using System;
using LanguageServer.Client;
using LanguageServer.Parameters;
using LanguageServer.Parameters.Workspace;

namespace LanguageServer.Server
{
  public class WorkspaceServiceTemplate : Service
  {
    public override Connection Connection
    {
      get => base.Connection;
      set
      {
        base.Connection = value;
        Proxy = new Proxy(value);
      }
    }

    public Proxy Proxy { get; private set; }

    // dynamicRegistration?: boolean;
    [JsonRpcMethod("workspace/didChangeConfiguration")]
    protected virtual void DidChangeConfiguration(DidChangeConfigurationParams @params)
    {
    }

    // dynamicRegistration?: boolean;
    [JsonRpcMethod("workspace/didChangeWatchedFiles")]
    protected virtual void DidChangeWatchedFiles(DidChangeWatchedFilesParams @params)
    {
    }

    // dynamicRegistration?: boolean;
    // Registration Options: void
    [JsonRpcMethod("workspace/symbol")]
    protected virtual Result<SymbolInformation[], ResponseError> Symbol(WorkspaceSymbolParams @params)
    {
      throw new NotImplementedException();
    }

    // dynamicRegistration?: boolean;
    // Registration Options: ExecuteCommandRegistrationOptions
    [JsonRpcMethod("workspace/executeCommand")]
    protected virtual Result<dynamic, ResponseError> ExecuteCommand(ExecuteCommandParams @params)
    {
      throw new NotImplementedException();
    }
  }
}