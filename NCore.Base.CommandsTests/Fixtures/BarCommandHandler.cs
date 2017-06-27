using System;
using System.Threading.Tasks;
using NCore.Base.Commands;

namespace NCore.Base.CommandsTests.Fixtures
{
  public class BarCommandHandler : ICommandHandler<BarCommand, BarResult>
  {
    public Task<BarResult> Execute(BarCommand command)
    {
      return Task.FromResult(new BarResult()
      {
        Result = command.Value + 1
      });
    }
  }
}