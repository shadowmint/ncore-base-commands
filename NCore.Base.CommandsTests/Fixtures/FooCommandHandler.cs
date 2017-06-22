using System;
using System.Threading.Tasks;
using NCore.Base.Commands;

namespace NCore.Base.CommandsTests.Fixtures
{
  public class FooCommandHandler : ICommandHandler<FooCommand>
  {
    public Task Execute(ICommand command)
    {
      return Task.Run(() => { });
    }
  }
}