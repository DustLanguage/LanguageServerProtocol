using System;
using System.Reflection;
using System.Threading;

namespace LanguageServer
{
  internal abstract class HandlerProvider
  {
    internal void AddHandlers(Handlers handlers, Type type)
    {
      TypeInfo methodCallType = typeof(MethodCall).GetTypeInfo();
      foreach (MethodInfo method in type.GetRuntimeMethods())
      {
        string rpcMethod = method.GetCustomAttribute<JsonRpcMethodAttribute>()?.Method;
        if (rpcMethod != null) AddHandler(handlers, type, method, rpcMethod);
      }
    }

    internal void AddHandler(Handlers handlers, Type type, MethodInfo method, string rpcMethod)
    {
      if (Reflector.IsRequestHandler(method))
      {
        RequestHandlerDelegate requestHandlerDelegate = Reflector.CreateRequestHandlerDelegate(type, method, this);
        RequestHandler requestHandler = new RequestHandler(rpcMethod, Reflector.GetRequestType(method), Reflector.GetResponseType(method), requestHandlerDelegate);
        handlers.AddRequestHandler(requestHandler);
      }
      else if (Reflector.IsNotificationHandler(method))
      {
        NotificationHandlerDelegate notificationHandlerDelegate = Reflector.CreateNotificationHandlerDelegate(type, method, this);
        NotificationHandler notificationHandler = new NotificationHandler(rpcMethod, Reflector.GetNotificationType(method), notificationHandlerDelegate);
        handlers.AddNotificationHandler(notificationHandler);
      }
    }

    internal abstract object CreateTargetObject(Type targetType, Connection connection, CancellationToken token);

    internal abstract object CreateTargetObject(Type targetType, Connection connection);
  }

  internal class ServiceHandlerProvider : HandlerProvider
  {
    internal override object CreateTargetObject(Type targetType, Connection connection, CancellationToken token)
    {
      Service svc = (Service) Activator.CreateInstance(targetType);
      svc.Connection = connection;
      svc.CancellationToken = token;
      return svc;
    }

    internal override object CreateTargetObject(Type targetType, Connection connection)
    {
      Service svc = (Service) Activator.CreateInstance(targetType);
      svc.Connection = connection;
      return svc;
    }
  }

  internal class ConnectionHandlerProvider : HandlerProvider
  {
    internal override object CreateTargetObject(Type targetType, Connection connection, CancellationToken token)
    {
      ServiceConnection sc = (ServiceConnection) connection;
      sc.CancellationToken = token;
      return sc;
    }

    internal override object CreateTargetObject(Type targetType, Connection connection)
    {
      return connection;
    }
  }
}