using System;
using System.Collections.Generic;
using System.Threading;
using LanguageServer.Json;

namespace LanguageServer
{
  internal class Handlers
  {
    private readonly SyncDictionary<NumberOrString, CancellationTokenSource> cancellations = new SyncDictionary<NumberOrString, CancellationTokenSource>();
    private readonly Dictionary<string, NotificationHandler> notificationHandlers = new Dictionary<string, NotificationHandler>();
    private readonly Dictionary<string, RequestHandler> requestHandlers = new Dictionary<string, RequestHandler>();
    private readonly SyncDictionary<NumberOrString, ResponseHandler> responseHandlers = new SyncDictionary<NumberOrString, ResponseHandler>();

    internal void AddRequestHandler(RequestHandler requestHandler)
    {
      requestHandlers[requestHandler.RpcMethod] = requestHandler;
    }

    internal void AddRequestHandlers(IEnumerable<RequestHandler> requestHandlers)
    {
      foreach (RequestHandler handler in requestHandlers) AddRequestHandler(handler);
    }

    internal bool TryGetRequestHandler(string method, out RequestHandler requestHandler)
    {
      return requestHandlers.TryGetValue(method, out requestHandler);
    }

    internal void AddResponseHandler(ResponseHandler responseHandler)
    {
      responseHandlers.Set(responseHandler.Id, responseHandler);
    }

    internal bool TryRemoveResponseHandler(NumberOrString id, out ResponseHandler responseHandler)
    {
      return responseHandlers.TryRemove(id, out responseHandler);
    }

    internal void AddNotificationHandler(NotificationHandler notificationHandler)
    {
      notificationHandlers[notificationHandler.RpcMethod] = notificationHandler;
    }

    internal void AddNotificationHandlers(IEnumerable<NotificationHandler> notificationHandlers)
    {
      foreach (NotificationHandler handler in notificationHandlers) AddNotificationHandler(handler);
    }

    internal bool TryGetNotificationHandler(string method, out NotificationHandler notificationHandler)
    {
      return notificationHandlers.TryGetValue(method, out notificationHandler);
    }

    internal void AddCancellationTokenSource(NumberOrString id, CancellationTokenSource tokenSource)
    {
      cancellations.Set(id, tokenSource);
    }

    internal void RemoveCancellationTokenSource(NumberOrString id)
    {
      cancellations.Remove(id);
    }

    internal bool TryRemoveCancellationTokenSource(NumberOrString id, out CancellationTokenSource tokenSource)
    {
      return cancellations.TryRemove(id, out tokenSource);
    }
  }

  internal class ResponseHandler
  {
    private readonly ResponseHandlerDelegate handler;

    internal ResponseHandler(NumberOrString id, Type responseType, ResponseHandlerDelegate handler)
    {
      Id = id;
      ResponseType = responseType;
      this.handler = handler;
    }

    internal NumberOrString Id { get; }

    internal Type ResponseType { get; }

    internal void Handle(object response)
    {
      handler(response);
    }
  }

  internal delegate void ResponseHandlerDelegate(object response);

  internal class RequestHandler
  {
    private readonly RequestHandlerDelegate handler;

    internal RequestHandler(string rpcMethod, Type requestType, Type responseType, RequestHandlerDelegate handler)
    {
      RpcMethod = rpcMethod;
      RequestType = requestType;
      ResponseType = responseType;
      this.handler = handler;
    }

    internal string RpcMethod { get; }

    internal Type RequestType { get; }

    internal Type ResponseType { get; }

    internal object Handle(object request, Connection connection, CancellationToken token)
    {
      return handler(request, connection, token);
    }
  }

  internal delegate object RequestHandlerDelegate(object request, Connection connection, CancellationToken token);

  internal class NotificationHandler
  {
    private readonly NotificationHandlerDelegate handler;

    internal NotificationHandler(string rpcMethod, Type notificationType, NotificationHandlerDelegate handler)
    {
      RpcMethod = rpcMethod;
      NotificationType = notificationType;
      this.handler = handler;
    }

    internal string RpcMethod { get; }

    internal Type NotificationType { get; }

    internal void Handle(object notification, Connection connection)
    {
      handler(notification, connection);
    }
  }

  internal delegate void NotificationHandlerDelegate(object notification, Connection connection);
}