using System;
using System.Collections.Generic;
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
    private IContainer Fixture(IEnumerable<string> regex)
    {
      var builder = new ContainerBuilder();
      new ServiceLocator(regex, true).RegisterAllByConvention(builder);
      var container = builder.Build();
      CommandService.RegisterSingleton(container);
      return container;
    }

    private IContainer Fixture(string regex)
    {
      var builder = new ContainerBuilder();
      new ServiceLocator(regex, true).RegisterAllByConvention(builder);
      var container = builder.Build();
      CommandService.RegisterSingleton(container);
      return container;
    }

    [Fact]
    public void CanFindWithValidRegex()
    {
      var container = Fixture("NCore\\.Base\\..*");
      container.Resolve<ISampleService>();
    }

    [Fact]
    public void CanFindWithValidRegexSet()
    {
      var container = Fixture(new[]
      {
        "^NCore\\.Base$",
        "^NCore\\.Base\\.Commands.*",
      });
      container.Resolve<ISampleService>();
    }

    [Fact]
    public void CanResolveCommandHandler()
    {
      var container = Fixture("NCore\\.Base\\..*");
      var commands = container.Resolve<ICommandService>();
      commands.Execute<FooCommand>().Wait();
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