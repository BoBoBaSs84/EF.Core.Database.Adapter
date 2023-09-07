using System.Globalization;
using System.Security.Cryptography;
using System.Text;

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
	public static string RemoveWhitespace(this string stringValue) =>
		Statics.WhitespaceRegex.Replace(stringValue, string.Empty);

	/// <summary>
	/// Formats the string with <paramref name="parameters"/> an invariant culture.
	/// </summary>
	/// <param name="stringValue">The string with placeholders.</param>
	/// <param name="parameters">The parameters to set for the placeholders.</param>
	/// <returns>Formated string.</returns>
	public static string FormatInvariant(this string stringValue, params object[] parameters) =>
		string.Format(CultureInfo.InvariantCulture, stringValue, parameters);

	/// <summary>
	/// Formats the string with <paramref name="parameters"/> an current culture.
	/// </summary>
	/// <param name="stringValue">The string with placeholders.</param>
	/// <param name="formatProvider">The format provider to use.</param>
	/// <param name="parameters">The parameters to set for the placeholders.</param>
	/// <returns>Formated string.</returns>
	public static string Format(this string stringValue, IFormatProvider formatProvider, params object[] parameters) =>
		string.Format(formatProvider, stringValue, parameters);

	/// <summary>
	/// Gets a string with the MD5 hash value of a given string (UT8 Encoded)
	/// </summary>
	/// <param name="inputString">The input string to work with.</param>
	/// <returns>The MD5 hashed string.</returns>
	public static string GetMd5Utf8(this string inputString) =>
		GetMd5Bytes(inputString, Encoding.UTF8).GetHexString();

	/// <summary>
	/// Gets a string with the MD5 hash value of a given string (ASCII Encoded)
	/// </summary>
	/// <param name="inputString">The input string to work with.</param>
	/// <returns>The MD5 hashed string.</returns>
	public static string GetMd5Ascii(this string inputString) =>
		GetMd5Bytes(inputString, Encoding.ASCII).GetHexString();

	/// <summary>
	/// Gets a string with the MD5 hash value of a given string (Unicode Encoded)
	/// </summary>
	/// <param name="inputString">The input string to work with.</param>
	/// <returns>The MD5 hashed string.</returns>
	public static string GetMd5Unicode(this string inputString) =>
		GetMd5Bytes(inputString, Encoding.Unicode).GetHexString();

	private static byte[] GetMd5Bytes(string inputString, Encoding encoding)
		=> MD5.HashData(encoding.GetBytes(inputString));
}
