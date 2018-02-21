using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;

namespace NCore.Base.Commands.Conventions
{
  public class ClassLocator
  {
    private List<Assembly> _assemblyList;

    private readonly List<Regex> _matchers;

    private List<Assembly> AssemblyList => _assemblyList ?? (_assemblyList = CollectAssemblies().ToList());

    public ClassLocator(IEnumerable<string> assemblyMatcherRegex)
    {
      _matchers = assemblyMatcherRegex.Select(i => new Regex(i)).ToList();
    }

    private IEnumerable<Assembly> CollectAssemblies()
    {
      AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(i => Debug.WriteLine(i));
      return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => _matchers.Any(i => i.IsMatch(assembly.FullName)));
    }

    public IEnumerable<Type> Implements<T>()
    {
      return from assembly in AssemblyList from type in assembly.GetTypes() where Implements<T>(type) select type;
    }

    public static bool Implements<T>(Type type)
    {
      return type != null && type.IsAssignableTo<T>() && type.IsPublic && !type.IsInterface && !type.IsAbstract;
    }
  }
}