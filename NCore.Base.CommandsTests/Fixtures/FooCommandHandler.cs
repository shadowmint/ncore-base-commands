using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NCore.Base.Commands;
using NCore.Base.CommandsTests.Fixtures.Services;

namespace NCore.Base.CommandsTests.Fixtures
{
  public class FooCommandHandler : ICommandHandler<FooCommand>
  {
    private readonly ISampleService _sample;
    private readonly ISampleSingleton _single;
    private readonly SampleConcrete _concrete;

    public FooCommandHandler(ISampleService sample, ISampleSingleton single, SampleConcrete concrete)
    {
      _sample = sample;
      _single = single;
      _concrete = concrete;
    }

    public Task Execute(FooCommand command)
    {
      return Task.Run(() =>
      {
        Debug.WriteLine(_sample.Value);
        Debug.WriteLine(_single.Value);
        Debug.WriteLine(_concrete.Value);
      });
    }
  }
}