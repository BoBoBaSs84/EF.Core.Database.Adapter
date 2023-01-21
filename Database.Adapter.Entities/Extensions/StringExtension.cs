using static Database.Adapter.Entities.Statics;

namespace Database.Adapter.Entities.Extensions;

/// <summary>
/// The string extension class.
/// </summary>
public static class StringExtension
{
	/// <summary>
	/// Should remove unwanted whitespace within a string.
	/// </summary>
	/// <param name="stringValue">The string to modify.</param>
	/// <returns></returns>
	public static string RemoveWhitespace(this string stringValue) => 
		WhitespaceRegex.Replace(stringValue, string.Empty);
}
