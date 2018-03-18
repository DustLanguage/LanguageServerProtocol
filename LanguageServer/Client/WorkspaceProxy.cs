using System.Threading.Tasks;
using LanguageServer.Parameters.Workspace;

namespace LanguageServer.Client
{
  public class WorkspaceProxy
  {
    private readonly Connection connection;

    internal WorkspaceProxy(Connection connection)
    {
      this.connection = connection;
    }

    public Task<Result<ApplyWorkspaceEditResponse, ResponseError>> ApplyEdit(ApplyWorkspaceEditParams @params)
    {
      TaskCompletionSource<Result<ApplyWorkspaceEditResponse, ResponseError>> tcs = new TaskCompletionSource<Result<ApplyWorkspaceEditResponse, ResponseError>>();
      connection.SendRequest(
        new RequestMessage<ApplyWorkspaceEditParams>
        {
          Id = IdGenerator.instance.Next(),
          Method = "workspace/applyEdit",
          Params = @params
        },
        (ResponseMessage<ApplyWorkspaceEditResponse, ResponseError> res) => tcs.TrySetResult(Message.ToResult(res)));
      return tcs.Task;
    }
  }
}