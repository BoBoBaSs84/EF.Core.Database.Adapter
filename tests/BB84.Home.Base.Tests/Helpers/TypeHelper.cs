﻿using System.Reflection;

namespace BB84.Home.Base.Tests.Helpers;

public static class TypeHelper
{
	public static IEnumerable<Type> GetAssemblyTypes(Assembly assembly, Func<Type, bool>? expression = null) =>
		expression is not null ? assembly.GetTypes().Where(expression).ToList() : [.. assembly.GetTypes()];
}
