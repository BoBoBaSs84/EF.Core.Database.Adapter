using static DA.Domain.Statics;

namespace DA.Domain.Extensions;

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
	public static string RemoveWhitespace(this string stringValue) =>
		WhitespaceRegex.Replace(stringValue, string.Empty);

	/// <summary>
	/// Formats the string with <paramref name="parameters"/> an invariant culture.
	/// </summary>
	/// <param name="stringValue">The string with placeholders.</param>
	/// <param name="parameters">The parameters to set for the placeholders.</param>
	/// <returns>Formated string.</returns>
	public static string FormatInvariant(this string stringValue, params object[] parameters) =>
		string.Format(InvariantCulture, stringValue, parameters);

	/// <summary>
	/// Formats the string with <paramref name="parameters"/> an current culture.
	/// </summary>
	/// <param name="stringValue">The string with placeholders.</param>
	/// <param name="formatProvider">The format provider to use.</param>
	/// <param name="parameters">The parameters to set for the placeholders.</param>
	/// <returns>Formated string.</returns>
	public static string Format(this string stringValue, IFormatProvider formatProvider, params object[] parameters) =>
		string.Format(formatProvider, stringValue, parameters);
}
