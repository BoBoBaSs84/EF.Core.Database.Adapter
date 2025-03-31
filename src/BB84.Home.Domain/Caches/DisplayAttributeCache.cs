using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BB84.Home.Domain.Caches;

/// <summary>
/// The display attribute cache class.
/// </summary>
internal static class DisplayAttributeCache<T> where T : struct, IComparable, IFormattable, IConvertible
{
	private static readonly Dictionary<T, string> Descriptions = [];
	private static readonly Dictionary<T, string> Names = [];
	private static readonly Dictionary<T, string> ShortNames = [];

	/// <summary>
	/// Initializes a instance of the display attribute cache class.
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
