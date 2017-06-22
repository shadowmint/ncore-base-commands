using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;

namespace NCore.Base.Commands
{
  public class CommandService
  {
    private readonly IContainer _container;

    public CommandService(IContainer container)
    {
      _container = container;
    }

    public Task Execute<T>() where T : ICommand
    {
      return Execute(Activator.CreateInstance<T>());
    }

    public Task Execute<T>(T command) where T : ICommand
    {
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
  }
}