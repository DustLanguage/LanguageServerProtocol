using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LanguageServer
{
  public class Service
  {
    public virtual Connection Connection { get; set; }

    public CancellationToken CancellationToken { get; set; }

    public static void Register(Connection connection, Type[] serviceTypes)
    {
      TypeInfo rpcType = typeof(Service).GetTypeInfo();
      if (serviceTypes.Any(x => !rpcType.IsAssignableFrom(x.GetTypeInfo()))) throw new ArgumentException("Specify types derived from JsonRpcService", nameof(serviceTypes));
      ServiceHandlerProvider provider = new ServiceHandlerProvider();
      foreach (Type serviceType in serviceTypes) provider.AddHandlers(connection.Handlers, serviceType);
    }

    public static void Register(Connection connection, Type serviceType)
    {
      ServiceHandlerProvider provider = new ServiceHandlerProvider();
      provider.AddHandlers(connection.Handlers, serviceType);
    }
  }
}