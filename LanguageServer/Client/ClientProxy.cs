using System.Threading.Tasks;
using LanguageServer.Parameters.Client;

namespace LanguageServer.Client
{
  public sealed class ClientProxy
  {
    private readonly Connection connection;

    internal ClientProxy(Connection connection)
    {
      this.connection = connection;
    }

    public Task<VoidResult<ResponseError>> RegisterCapability(RegistrationParams @params)
    {
      TaskCompletionSource<VoidResult<ResponseError>> tcs = new TaskCompletionSource<VoidResult<ResponseError>>();
      connection.SendRequest(
        new RequestMessage<RegistrationParams>
        {
          Id = IdGenerator.instance.Next(),
          Method = "client/registerCapability",
          Params = @params
        },
        (VoidResponseMessage<ResponseError> res) => tcs.TrySetResult(Message.ToResult(res)));
      return tcs.Task;
    }

    public Task<VoidResult<ResponseError>> UnregisterCapability(UnregistrationParams @params)
    {
      TaskCompletionSource<VoidResult<ResponseError>> tcs = new TaskCompletionSource<VoidResult<ResponseError>>();
      connection.SendRequest(
        new RequestMessage<UnregistrationParams>
        {
          Id = IdGenerator.instance.Next(),
          Method = "client/unregisterCapability",
          Params = @params
        },
        (VoidResponseMessage<ResponseError> res) => tcs.TrySetResult(Message.ToResult(res)));
      return tcs.Task;
    }
  }
}