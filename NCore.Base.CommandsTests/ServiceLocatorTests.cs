using System;
using Autofac;
using Autofac.Core.Registration;
using NCore.Base.Commands;
using NCore.Base.Commands.Conventions;
using NCore.Base.CommandsTests.Fixtures;
using NCore.Base.CommandsTests.Fixtures.Services;
using Xunit;

namespace NCore.Base.CommandsTests
{
  public class ServiceLocatorTests
  {
    private IContainer Fixture(string regex)
    {
      var builder = new ContainerBuilder();
      new ServiceLocator(regex).RegisterAllByConvention(builder);
      return builder.Build();
    }

    [Fact]
    public void CanFindWithValidRegex()
    {
      var container = Fixture("NCore\\.Base\\..*");
      container.Resolve<ISampleService>();
    }

    [Fact]
    public void CannotFindTypeWithNoMatchingAssemblies()
    {
      var container = Fixture("XXXX\\.Base\\..*");
      Assert.Throws<ComponentNotRegisteredException>(() => container.Resolve<ISampleService>());
    }

    [Fact]
    public void CannotUseInvalidRegex()
    {
      Assert.Throws<ArgumentException>(() => Fixture("[[*"));
    }
  }
}