using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Registration;
using NCore.Base.Commands.Conventions;

namespace NCore.Base.Commands
{
  public class CommandService : ICommandService, ISingleton
  {
    private IContainer _container;

    public CommandService()
    {
    }

    public CommandService(IContainer container)
    {
      _container = container;
    }

    private void Initialize(IContainer container)
    {
      _container = container;
    }

    public Task Execute<T>() where T : ICommand
    {
      return Execute(Activator.CreateInstance<T>());
    }

    public Task Execute<T>(T command) where T : ICommand
    {
      GuardContainerNotNull();
      try
      {
        var resolver = _container.Resolve<ICommandHandler<T>>();
        return resolver.Execute(command);
      }
      catch (Exception error)
      {
        ThrowCommandFailed(error);
      }

      return default(Task);
    }

    public async Task<TResult> Execute<T, TResult>() where T : ICommand
    {
      return await Execute<T, TResult>(Activator.CreateInstance<T>());
    }

    public async Task<TResult> Execute<T, TResult>(T command) where T : ICommand
    {
      GuardContainerNotNull();
      try
      {
        var resolver = _container.Resolve<ICommandHandler<T, TResult>>();
        return await resolver.Execute(command);
      }
      catch (Exception error)
      {
        ThrowCommandFailed(error);
      }

      return default(TResult);
    }

    private static void ThrowCommandFailed(Exception error)
    {
      throw new CommandFailedException("Error executing command", error);
    }

    private void GuardContainerNotNull()
    {
      if (_container == null)
      {
        ThrowCommandFailed(new Exception("Error executing command: CommandService not initialized"));
      }
    }

    public static void RegisterSingleton(IContainer container)
    {
      try
      {
        var service = container.Resolve<ICommandService>() as CommandService;
        service?.Initialize(container);
      }
      catch (ComponentNotRegisteredException)
      {
        // Probably a regex or regex set that did not include NCore.Base.Commands
      }
    }
  }
}