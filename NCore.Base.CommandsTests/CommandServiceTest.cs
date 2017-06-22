using System;
using Autofac;
using NCore.Base.Commands;
using NCore.Base.CommandsTests.Fixtures;
using Xunit;

namespace NCore.Base.CommandsTests
{
  public class CommandServiceTest
  {
    private IContainer Fixture()
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<BarCommandHandler>().AsImplementedInterfaces().SingleInstance();
      builder.RegisterType<FooCommandHandler>().AsImplementedInterfaces().SingleInstance();
      return builder.Build();
    }

    [Fact]
    public void CanExecuteBoundCommand()
    {
      var service = new CommandService(Fixture());

      service.Execute<FooCommand>().Wait();
      service.Execute(new FooCommand()).Wait();
    }

    [Fact]
    public async void CanExecuteCommandWithResult()
    {
      var service = new CommandService(Fixture());

      var result = await service.Execute<BarCommand, BarResult>();
      Assert.Equal(result.Result, 1);

      result = await service.Execute<BarCommand, BarResult>(new BarCommand() {Value = 10});
      Assert.Equal(result.Result, 11);
    }

    [Fact]
    public void CantExecuteUnboundCommand()
    {
      var service = new CommandService(Fixture());
      Assert.Throws<CommandFailedException>(() => { service.Execute<NotImplCommand>().Wait(); });
    }
    
    [Fact]
    public void CantExecuteCommandForUnboundResponseType()
    {
      var service = new CommandService(Fixture());
      Assert.Throws<CommandFailedException>(() =>
      {
        try
        {
          var result = service.Execute<BarCommand, FooCommand>();
          result.Wait();
        }
        catch (AggregateException exception)
        {
          Assert.Equal(exception.InnerExceptions.Count, 1);
          throw exception.InnerExceptions[0];
        }
      });
    }
  }
}