using NCore.Base.Commands.Conventions;

namespace NCore.Base.CommandsTests.Fixtures.Services
{
  public class SampleConcrete : IConcrete
  {
    public string Value { get; } = "Concrete Value";
  }
}