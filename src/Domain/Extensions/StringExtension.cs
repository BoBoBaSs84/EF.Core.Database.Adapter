using Domain.Statics;

namespace Domain.Extensions;

/// <summary>
/// The string extension class.
/// </summary>
public static class StringExtension
{
	/// <summary>
	/// Should remove unwanted whitespace within a string.
	/// </summary>
	/// <param name="stringValue">The string to modify.</param>
	/// <returns>The replaced string.</returns>
	public static string RemoveWhitespace(this string stringValue)
		=> RegexStatics.Whitespace.Replace(stringValue, string.Empty);
}
