using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Autofac;
using Autofac.Core;

namespace NCore.Base.Commands.Conventions
{
  public class ServiceLocator
  {
    private readonly ClassLocator _classLocator;

    public ServiceLocator(string assemblyMatchRegex = ".*")
    {
      _classLocator = new ClassLocator(assemblyMatchRegex);
    }

    public void RegisterAllByConvention(ContainerBuilder builder)
    {
      RegisterServices(builder);
      RegisterSingletons(builder);
      RegisterConcreteInstances(builder);
      RegisterCommandHandlers(builder);
    }

    private void RegisterCommandHandlers(ContainerBuilder builder)
    {
      foreach (var type in _classLocator.Implements<ICommandHandler>())
      {
        if (ClassLocator.Implements<ISingleton>(type))
        {
          builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
        }
        else
        {
          builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
        }
      }
    }

    private void RegisterConcreteInstances(ContainerBuilder builder)
    {
      foreach (var type in _classLocator.Implements<IConcrete>())
      {
        builder.RegisterType(type).AsSelf().SingleInstance();
      }
    }

    private void RegisterSingletons(ContainerBuilder builder)
    {
      foreach (var type in _classLocator.Implements<ISingleton>())
      {
        builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
      }
    }

    private void RegisterServices(ContainerBuilder builder)
    {
      foreach (var type in _classLocator.Implements<IService>())
      {
        builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
      }
    }
  }
}