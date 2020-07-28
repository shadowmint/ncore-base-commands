using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Autofac;
using Autofac.Core;
using NCore.Base.Commands.Utility;

namespace NCore.Base.Commands.Conventions
{
    public class ServiceLocator
    {
        private readonly bool _debug;
        private readonly ClassLocator _classLocator;

        public ServiceLocator(string assemblyMatchRegex = ".*", bool debug = false)
        {
            _debug = debug;
            _classLocator = new ClassLocator(new[] {"^NCore\\.Base\\.Commands$", assemblyMatchRegex}, debug);
        }

        public ServiceLocator(IEnumerable<string> assemblyMatchRegex, bool debug = false)
        {
            _debug = debug;
            _classLocator = new ClassLocator(assemblyMatchRegex, debug);
        }

        public void RegisterAllByConvention(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterSingletons(builder);
            RegisterConcreteInstances(builder);
            RegisterCommandHandlers(builder);
        }

        private void RegisterCommandHandlers(ContainerBuilder builder)
        {
            foreach (var type in _classLocator.Implements<ICommandHandler>())
            {
                if (ClassLocator.Implements<ISingleton>(type))
                {
                    Trace($"register command handler: {type.Name} -> ImplementedInterfaces, SingleInstance");
                    builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                }
                else
                {
                    Trace($"register command handler: {type.Name} -> ImplementedInterfaces, InstancePerDependency");
                    builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
                }
            }
        }

        private void RegisterConcreteInstances(ContainerBuilder builder)
        {
            foreach (var type in _classLocator.Implements<IConcrete>())
            {
                if (ClassLocator.Implements<ISingleton>(type))
                {
                    Trace($"register concrete: {type.Name} -> AsSelf, SingleInstance");
                    builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                }
                else
                {
                    Trace($"register concrete: {type.Name} -> AsSelf, InstancePerDependency");
                    builder.RegisterType(type).AsSelf().InstancePerDependency();
                }
            }
        }

        private void RegisterSingletons(ContainerBuilder builder)
        {
            foreach (var type in _classLocator.Implements<ISingleton>())
            {
                Trace($"register singleton: {type.Name} -> AsImplementedInterfaces, SingleInstance");
                builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
            }
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            foreach (var type in _classLocator.Implements<IService>())
            {
                Trace($"register service: {type.Name} -> AsImplementedInterfaces, InstancePerDependency");
                builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
            }
        }

        private void Trace(string message)
        {
            if (_debug)
            {
                Console.WriteLine($"{GetType().FullName}: {message}");
            }
        }
    }
}