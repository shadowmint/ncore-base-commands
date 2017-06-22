using System.Threading.Tasks;

namespace NCore.Base.Commands
{
  public interface ICommandHandler<T> where T : ICommand
  {
    Task Execute(ICommand command);
  }

  public interface ICommandHandler<T, TResult> where T : ICommand
  {
    Task<TResult> Execute(ICommand command);
  }
}