using NCore.Base.Commands.Conventions;

namespace NCore.Base.CommandsTests.Fixtures.Services
{
  public class SampleSingleton : ISampleSingleton, ISingleton
  {
    public string Value => "Singleton Value";
  }
}