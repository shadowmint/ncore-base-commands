using System.Threading.Tasks;

namespace NCore.Base.Commands
{
  public interface ICommandHandler
  {
  }

  public interface ICommandHandler<in T> : ICommandHandler where T : ICommand
  {
    Task Execute(T command);
  }

  public interface ICommandHandler<in T, TResult> : ICommandHandler where T : ICommand
  {
    Task<TResult> Execute(T command);
  }
}