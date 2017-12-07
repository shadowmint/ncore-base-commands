﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;

namespace NCore.Base.Commands.Conventions
{
  public class ClassLocator
  {
    private List<Assembly> _assemblyList;

    private readonly Regex _match;

    private List<Assembly> AssemblyList => _assemblyList ?? (_assemblyList = CollectAssemblies().ToList());

    public ClassLocator(string assemblyMatcherRegex = ".*")
    {
      _match = new Regex(assemblyMatcherRegex);
    }

    private IEnumerable<Assembly> CollectAssemblies()
    {
      return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => _match.IsMatch(assembly.FullName));
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