# NCore.Base.Commands

Basic command and command handler infrastructure using Autofac using conventions.

You can implement the following interfaces:

 - IService
 - IConcrete
 - ISingleton
 - ICommandHandler<TCommand>
 - ICommand
 
To automatically register dependencies using discovery use:

    var builder = new ContainerBuilder();
    new ServiceLocator(regex).RegisterAllByConvention(builder);
    return builder.Build();

You can also use the extension methods for a quickstart:

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
 
See [Program.cs](https://github.com/shadowmint/ncore-base-commands/blob/master/NCore.Base.CommandsDemo/Program.cs) for a full example.
         
# Installing

    npm install --save shadowmint/ncore-base-commands

Now add the `NuGet.Config` to the project folder:

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
     <packageSources>
        <add key="local" value="./packages" />
     </packageSources>
    </configuration>

Now you can install the package:

    dotnet add package NCore.Base.Commands

You may also want to use `dotnet nuget locals all --clear` to clear cached objects.

# Building a new package version

    npm run build

# Testing

    npm test
