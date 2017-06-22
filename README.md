# NCore.Base.Commands

Basic command and command handler infrastructure using Autofac.

    using AutoFace;
    using NCore.Base.Commands;

    var builder = new ContainerBuilder();
    builder.RegisterType<BarCommandHandler>().AsImplementedInterfaces().SingleInstance();
    builder.RegisterType<FooCommandHandler>().AsImplementedInterfaces().SingleInstance();
    var container = builder.Build();

    var service = new CommandService(container);

    service.Execute<FooCommand>();
    service.Execute(new FooCommand() { ... });

    var result = await service.Execute<BarCommand, BarResult>();
    var result = await service.Execute<BarCommand, BarResult>(new BarCommand() { ... });

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
