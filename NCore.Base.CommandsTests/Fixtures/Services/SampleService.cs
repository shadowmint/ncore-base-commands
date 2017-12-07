using NCore.Base.Commands.Conventions;

namespace NCore.Base.CommandsTests.Fixtures.Services
{
  public class SampleService : ISampleService, IService
  {
    public string Value => "Service value";
  }
}