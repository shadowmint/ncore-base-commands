using System;
using System.Reflection;
using Autofac;
using NCore.Base.Commands.Conventions;

namespace NCore.Base.Commands.Utility
{
    public static class ContainerBuilderExtensions
    {
        public static IContainer RegisterByConvention(this ContainerBuilder builder, params string[] assemblyPatterns)
        {
            new ServiceLocator(assemblyPatterns).RegisterAllByConvention(builder);
            return builder.Build();
        }

        public static IContainer RegisterByConventionWithDebug(this ContainerBuilder builder, params string[] assemblyPatterns)
        {
            new ServiceLocator(assemblyPatterns, true).RegisterAllByConvention(builder);
            return builder.Build();
        }

        public static IContainer RegisterByConvention(this ContainerBuilder builder)
        {
            var pattern = Assembly.GetEntryAssembly()?.GetName().Name;
            if (pattern == null)
            {
                throw new NotSupportedException("Not entry assembly found; specify match patterns explicitly");
            }

            new ServiceLocator(pattern).RegisterAllByConvention(builder);
            return builder.Build();
        }
    }
}