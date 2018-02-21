using System.Threading.Tasks;

namespace NCore.Base.Commands
{
  public interface ICommandService
  {
    Task Execute<T>() where T : ICommand;
    Task Execute<T>(T command) where T : ICommand;
    Task<TResult> Execute<T, TResult>() where T : ICommand;
    Task<TResult> Execute<T, TResult>(T command) where T : ICommand;
  }
}