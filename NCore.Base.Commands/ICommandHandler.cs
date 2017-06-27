using System.Threading.Tasks;

namespace NCore.Base.Commands
{
  public interface ICommandHandler<in T> where T : ICommand
  {
    Task Execute(T command);
  }

  public interface ICommandHandler<in T, TResult> where T : ICommand
  {
    Task<TResult> Execute(T command);
  }
}