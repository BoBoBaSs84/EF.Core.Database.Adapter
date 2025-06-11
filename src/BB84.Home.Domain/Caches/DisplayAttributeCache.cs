using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BB84.Home.Domain.Caches;

/// <summary>
/// Provides a cached mechanism for retrieving display-related attributes, such as descriptions, names,
/// and short names, for enum values of type <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// This class is designed to improve performance by caching the display attributes of enum values.
/// It uses the <see cref="DisplayAttribute"/> applied to the enum members to populate the cache.
/// If an enum member does not have a <see cref="DisplayAttribute"/>, its name is used as the fallback
/// for descriptions, names, and short names.
/// </remarks>
/// <typeparam name="T">The enum type for which display attributes are cached.</typeparam>
internal static class DisplayAttributeCache<T> where T : struct, IComparable, IFormattable, IConvertible
{
	private static readonly Dictionary<T, string> Descriptions = [];
	private static readonly Dictionary<T, string> Names = [];
	private static readonly Dictionary<T, string> ShortNames = [];

	/// <summary>
	/// Initializes the static cache for the enum type <typeparamref name="T"/>.
	/// </summary>
	static DisplayAttributeCache()
	{
		Type type = typeof(T);

		foreach (T value in Enum.GetValues(type).Cast<T>())
		{
			string valueName = value.ToString()!;

			Descriptions.Add(value, type.GetMember(valueName)[0].GetCustomAttribute<DisplayAttribute>()?.GetDescription() ?? valueName);
			Names.Add(value, type.GetMember(valueName)[0].GetCustomAttribute<DisplayAttribute>()?.GetName() ?? valueName);
			ShortNames.Add(value, type.GetMember(valueName)[0].GetCustomAttribute<DisplayAttribute>()?.GetShortName() ?? valueName);
		}
	}

	/// <summary>
	/// Returns the description of the <paramref name="value"/> from the cache.
	/// </summary>
	/// <param name="value">The enum type value.</param>
	/// <returns>The enumerator description.</returns>
	internal static string GetDescription(T value)
		=> Descriptions[value];

	/// <summary>
	/// Returns the name of the <paramref name="value"/> from the cache.
	/// </summary>
	/// <param name="value">The enum type value.</param>
	/// <returns>The enumerator name.</returns>
	internal static string GetName(T value)
		=> Names[value];

	/// <summary>
	/// Returns the short name of the <paramref name="value"/> from the cache.
	/// </summary>
	/// <param name="value">The enum type value.</param>
	/// <returns>The enumerator short name.</returns>
	internal static string GetShortName(T value)
		=> ShortNames[value];
}
