using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using NCore.Base.Commands;
using NCore.Base.Commands.Conventions;
using NCore.Base.Commands.Utility;

namespace NCore.Base.CommandsDemo
{
    public class FooCommand : ICommand
    {
        public string Name { get; set; }
    }

    public interface IFooService
    {
        public void Hi(string name);
    }

    public class ServiceApi : IConcrete
    {
        private readonly IFooService _foo;

        public ServiceApi(IFooService foo)
        {
            _foo = foo;
        }

        public void Hello(string name)
        {
            _foo.Hi(name);
        }
    }

    public class FooService : IService, IFooService
    {
        public void Hi(string name)
        {
            Console.WriteLine($"Hi {name}");
        }
    }

    public class FooCommandHandler : ICommandHandler<FooCommand>
    {
        private readonly ServiceApi _api;

        public FooCommandHandler(ServiceApi api)
        {
            _api = api;
        }

        public Task Execute(FooCommand command)
        {
            _api.Hello(command.Name);
            return Task.CompletedTask;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var container = new ContainerBuilder().RegisterByConvention();

            var foo = container.Resolve<IFooService>();
            foo.Hi("Test");

            var commandService = new CommandService(container);
            await commandService.Execute(new FooCommand() {Name = "Test2"});
        }
    }
}