using LanguageServer.Parameters.TextDocument;

namespace LanguageServer.Client
{
  public class TextDocumentProxy
  {
    private readonly Connection connection;

    internal TextDocumentProxy(Connection connection)
    {
      this.connection = connection;
    }

    public void PublishDiagnostics(PublishDiagnosticsParams @params)
    {
      connection.SendNotification(new NotificationMessage<PublishDiagnosticsParams>
      {
        Method = "textDocument/publishDiagnostics",
        Params = @params
      });
    }
  }
}