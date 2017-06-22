using System;
using System.Threading.Tasks;
using NCore.Base.Commands;

namespace NCore.Base.CommandsTests.Fixtures
{
  public class BarCommandHandler : ICommandHandler<BarCommand, BarResult>
  {
    public Task<BarResult> Execute(ICommand command)
    {
      var c = command as BarCommand;
      return Task.FromResult(new BarResult()
      {
        Result = c.Value + 1
      });
    }
  }
}