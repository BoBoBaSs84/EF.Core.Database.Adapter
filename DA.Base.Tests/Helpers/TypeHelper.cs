using System.Reflection;

namespace DA.Base.Tests.Helpers;

public static class TypeHelper
{
	public static ICollection<Type> GetAssemblyTypes(Assembly assembly, Func<Type, bool>? expression = null) =>
		expression is not null ? assembly.GetTypes().Where(expression).ToList() : assembly.GetTypes().ToList();
}
