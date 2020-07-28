using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;

namespace NCore.Base.Commands.Utility
{
    public class ClassLocator
    {
        private readonly bool _debug;
        private List<Assembly> _assemblyList;

        private readonly List<Regex> _matchers;

        private List<Assembly> AssemblyList => _assemblyList ?? (_assemblyList = CollectAssemblies().ToList());

        public ClassLocator(IEnumerable<string> assemblyMatcherRegex, bool debug)
        {
            _debug = debug;
            try
            {
                _matchers = assemblyMatcherRegex.Select(i => new Regex(i)).ToList();
            }
            catch (Exception error)
            {
                throw new ArgumentException("Invalid regex", error);
            }
            
        }

        private IEnumerable<Assembly> CollectAssemblies()
        {
            foreach (var pattern in _matchers)
            {
                Trace($"Pattern: {pattern}");
            }
            return AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
            {
                Trace($"Found assembly: {assembly.GetName().Name}");
                return _matchers.Any(i =>
                {
                    if (!i.IsMatch(assembly.GetName().Name)) return false;
                    Trace($"Regex matched assembly: {assembly.FullName}");
                    return true;
                });
            });
        }

        public IEnumerable<Type> Implements<T>()
        {
            return from assembly in AssemblyList from type in assembly.GetTypes() where Implements<T>(type) select type;
        }

        public static bool Implements<T>(Type type)
        {
            return type != null && type.IsAssignableTo<T>() && type.IsPublic && !type.IsInterface && !type.IsAbstract;
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