using System;
using LanguageServer.Client;
using LanguageServer.Parameters.General;

namespace LanguageServer.Server
{
  public class GeneralServiceTemplate : Service
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

    [JsonRpcMethod("initialize")]
    protected virtual Result<InitializeResult, ResponseError<InitializeErrorData>> Initialize(InitializeParams @params)
    {
      throw new NotImplementedException();
    }

    [JsonRpcMethod("initialized")]
    protected virtual void Initialized()
    {
    }

    [JsonRpcMethod("shutdown")]
    protected virtual VoidResult<ResponseError> Shutdown()
    {
      throw new NotImplementedException();
    }

    [JsonRpcMethod("exit")]
    protected virtual void Exit()
    {
    }
  }
}