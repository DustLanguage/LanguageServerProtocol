using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LanguageServer.Json;
using LanguageServer.Parameters.General;
using Matarillo.IO;

namespace LanguageServer
{
  public class Connection
  {
    private const byte Cr = 13;
    private const byte Lf = 10;
    private readonly object outputLock = new object();
    private readonly byte[] separator = {Cr, Lf};
    private ProtocolReader input;
    private Stream output;

    public Connection(Stream input, Stream output)
    {
      this.input = new ProtocolReader(input);
      this.output = output;
    }

    internal Handlers Handlers { get; } = new Handlers();

    public async Task Listen()
    {
      while (await ReadAndHandle())
      {
      }
    }

    public async Task<bool> ReadAndHandle()
    {
      string json = await Read();
      MessageTest messageTest = (MessageTest) Serializer.Instance.Deserialize(typeof(MessageTest), json);
      if (messageTest == null) return false;
      if (messageTest.IsRequest)
        HandleRequest(messageTest.Method, messageTest.Id, json);
      else if (messageTest.IsResponse)
        HandleResponse(messageTest.Id, json);
      else if (messageTest.IsCancellation)
        HandleCancellation(json);
      else if (messageTest.IsNotification) HandleNotification(messageTest.Method, json);
      return true;
    }

    private void HandleRequest(string method, NumberOrString id, string json)
    {
      if (Handlers.TryGetRequestHandler(method, out RequestHandler handler))
        try
        {
          CancellationTokenSource tokenSource = new CancellationTokenSource();
          Handlers.AddCancellationTokenSource(id, tokenSource);
          object request = Serializer.Instance.Deserialize(handler.RequestType, json);
          ResponseMessageBase requestResponse = (ResponseMessageBase) handler.Handle(request, this, tokenSource.Token);
          Handlers.RemoveCancellationTokenSource(id);
          requestResponse.Id = id;
          SendMessage(requestResponse);
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine(ex);
          ResponseMessageBase requestErrorResponse = Reflector.CreateErrorResponse(handler.ResponseType, ex.ToString());
          SendMessage(requestErrorResponse);
        }
    }

    private void HandleResponse(NumberOrString id, string json)
    {
      if (Handlers.TryRemoveResponseHandler(id, out ResponseHandler handler))
      {
        object response = Serializer.Instance.Deserialize(handler.ResponseType, json);
        handler.Handle(response);
      }
    }

    private void HandleCancellation(string json)
    {
      NotificationMessage<CancelParams> cancellation = (NotificationMessage<CancelParams>) Serializer.Instance.Deserialize(typeof(NotificationMessage<CancelParams>), json);
      NumberOrString id = cancellation.Params.Id;
      if (Handlers.TryRemoveCancellationTokenSource(id, out CancellationTokenSource tokenSource)) tokenSource.Cancel();
    }

    private void HandleNotification(string method, string json)
    {
      if (Handlers.TryGetNotificationHandler(method, out NotificationHandler handler))
      {
        object notification = Serializer.Instance.Deserialize(handler.NotificationType, json);
        handler.Handle(notification, this);
      }
    }

    public void SendRequest<TRequest, TResponse>(TRequest request, Action<TResponse> responseHandler)
      where TRequest : RequestMessageBase
      where TResponse : ResponseMessageBase
    {
      ResponseHandler handler = new ResponseHandler(request.Id, typeof(TResponse), o => responseHandler((TResponse) o));
      Handlers.AddResponseHandler(handler);
      SendMessage(request);
    }

    public void SendNotification<TNotification>(TNotification notification)
      where TNotification : NotificationMessageBase
    {
      SendMessage(notification);
    }

    public void SendCancellation(NumberOrString id)
    {
      NotificationMessage<CancelParams> message = new NotificationMessage<CancelParams> {Method = "$/cancelRequest", Params = new CancelParams {Id = id}};
      SendMessage(message);
    }

    private void SendMessage(MessageBase message)
    {
      Write(Serializer.Instance.Serialize(typeof(MessageBase), message));
    }

    private void Write(string json)
    {
      byte[] utf8 = Encoding.UTF8.GetBytes(json);
      lock (outputLock)
      {
        using (StreamWriter writer = new StreamWriter(output, Encoding.ASCII, 1024, true))
        {
          writer.Write($"Content-Length: {utf8.Length}\r\n");
          writer.Write("\r\n");
          writer.Flush();
        }

        output.Write(utf8, 0, utf8.Length);
        output.Flush();
      }
    }

    private async Task<string> Read()
    {
      int contentLength = 0;
      byte[] headerBytes = await input.ReadToSeparatorAsync(separator);
      while (headerBytes.Length != 0)
      {
        string headerLine = Encoding.ASCII.GetString(headerBytes);
        int position = headerLine.IndexOf(": ");
        if (position >= 0)
        {
          string name = headerLine.Substring(0, position);
          string value = headerLine.Substring(position + 2);
          if (string.Equals(name, "Content-Length", StringComparison.Ordinal)) int.TryParse(value, out contentLength);
        }

        headerBytes = await input.ReadToSeparatorAsync(separator);
      }

      if (contentLength == 0) return "";
      byte[] contentBytes = await input.ReadBytesAsync(contentLength);
      return Encoding.UTF8.GetString(contentBytes);
    }
  }
}