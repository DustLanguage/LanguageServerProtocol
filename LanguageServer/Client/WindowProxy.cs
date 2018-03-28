using System.Threading.Tasks;
using LanguageServer.Parameters.Client;
using LanguageServer.Parameters.Window;

namespace LanguageServer.Client
{
  public sealed class WindowProxy
  {
    private readonly Connection connection;

    internal WindowProxy(Connection connection)
    {
      this.connection = connection;
    }

    public void ShowMessage(ShowMessageParams @params)
    {
      connection.SendNotification(new NotificationMessage<ShowMessageParams>
      {
        Method = "window/showMessage",
        Params = @params
      });
    }

    public Task<Result<MessageActionItem, ResponseError>> ShowMessageRequest(ShowMessageRequestParams @params)
    {
      TaskCompletionSource<Result<MessageActionItem, ResponseError>> tcs = new TaskCompletionSource<Result<MessageActionItem, ResponseError>>();
      connection.SendRequest(
        new RequestMessage<ShowMessageRequestParams>
        {
          Id = IdGenerator.instance.Next(),
          Method = "window/showMessageRequest",
          Params = @params
        },
        (ResponseMessage<MessageActionItem, ResponseError> res) => tcs.TrySetResult(Message.ToResult(res)));
      return tcs.Task;
    }

    public void LogMessage(LogMessageParams @params)
    {
      connection.SendNotification(new NotificationMessage<LogMessageParams>
      {
        Method = "window/logMessage",
        Params = @params
      });
    }

    public void Event(dynamic @params)
    {
      connection.SendNotification(new NotificationMessage<dynamic>
      {
        Method = "telemetry/event",
        Params = @params
      });
    }
  }
}