using System.Reflection;

namespace Database.Adapter.Base.Tests.Helpers;

public static class AttributeHelper
{
	public static bool TypeHasAttribute<TAttribute>(Type type)
		where TAttribute : Attribute =>
		type.GetCustomAttributes<TAttribute>().Any();

	public static bool MethodHasAttribute<TAttribute>(MethodInfo info)
		where TAttribute : Attribute =>
		info.GetCustomAttributes<TAttribute>().Any();
}
