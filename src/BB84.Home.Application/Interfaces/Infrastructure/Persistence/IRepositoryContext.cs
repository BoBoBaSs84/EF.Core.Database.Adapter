using BB84.EntityFrameworkCore.Repositories.Abstractions;

namespace BB84.Home.Application.Interfaces.Infrastructure.Persistence;

/// <summary>
/// The application context interface.
/// </summary>
public interface IRepositoryContext : IDbContext
{
	/// <summary>
	/// Returns the last day of the month for the specified date.
	/// </summary>
	/// <remarks>
	/// This method is intended for use in database queries and is mapped to the SQL function <c>EOMONTH</c>.
	/// It cannot be invoked directly in application code.
	/// </remarks>
	/// <param name="inputDate">The date for which the last day of the month is calculated.</param>
	/// <returns>
	/// A <see cref="DateTime"/> representing the last day of the month for the given <paramref name="inputDate"/>.
	/// </returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the method is invoked directly in application code.
	/// This method is intended for use in LINQ queries with Entity Framework.
	/// </exception>
	DateTime EndOfMonth(DateTime inputDate);

	/// <summary>
	/// Returns a string with the specified input value enclosed in delimiters,
	/// making it suitable for use as a SQL identifier.
	/// </summary>
	/// <remarks>
	/// This method corresponds to the SQL <c>QUOTENAME</c> function, which adds delimiters
	/// to a string to ensure it is treated as a valid SQL identifier.
	/// </remarks>
	/// <param name="inputValue">
	/// The input string to be quoted. Typically, this represents a SQL identifier such as a table or column name.
	/// </param>
	/// <returns>
	/// A string containing the input value enclosed in delimiters, or <see langword="null"/>
	/// if the input value is <see langword="null"/>.
	/// </returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the method is invoked directly in application code.
	/// This method is intended for use in LINQ queries with Entity Framework.
	/// </exception>
	string? QuoteName(string inputValue);

	/// <summary>
	/// Returns the Soundex code for the specified string.
	/// </summary>
	/// <remarks>
	/// The Soundex algorithm is used to encode words based on their phonetic similarity.
	/// This method is mapped to the SQL Server SOUNDEX function and should be used in LINQ-to-Entities queries.
	/// </remarks>
	/// <param name="inputValue">The input string for which the Soundex code is calculated. Must not be null.</param>
	/// <returns>A string representing the Soundex code of the input value.</returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the method is invoked directly in application code.
	/// This method is intended for use in LINQ queries with Entity Framework.
	/// </exception>
	string Soundex(string inputValue);

	/// <summary>
	/// Calculates the difference between the phonetic representations of two strings using the
	/// SQL Server <c>DIFFERENCE</c> function.
	/// </summary>
	/// <remarks>
	/// This method maps to the SQL Server <c>DIFFERENCE</c> function, which compares the
	/// <c>SOUNDEX</c> values of two strings. It is intended for use in LINQ queries to execute
	/// database-side logic.
	/// </remarks>
	/// <param name="inputValue">The first string to compare. Cannot be null.</param>
	/// <param name="valueToCompare">The second string to compare. Cannot be null.</param>
	/// <returns>
	/// An integer value representing the similarity between the phonetic representations of the two strings.
	/// Higher values indicate greater similarity, with a maximum value of 4.
	/// </returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the method is invoked directly in application code.
	/// This method is intended for use in LINQ queries with Entity Framework.
	/// </exception>
	int Difference(string inputValue, string valueToCompare);

	/// <summary>
	/// Replaces characters in the specified input string with corresponding characters from a translation string.
	/// </summary>
	/// <remarks>
	/// The <c>Translate</c> function is typically used to perform character-by-character substitutions in a string.
	/// The lengths of <paramref name="characters"/> and <paramref name="translations"/> must match; otherwise, the
	/// behavior is undefined.
	/// </remarks>
	/// <param name="inputValue">The input string to be processed. Cannot be null.</param>
	/// <param name="characters">A string containing the characters to be replaced. Each character in this string
	/// is mapped to the corresponding character in <paramref name="translations"/>. Cannot be null.</param>
	/// <param name="translations">A string containing the replacement characters. Each character in this string
	/// corresponds to the character at the same position in <paramref name="characters"/>. Cannot be null.</param>
	/// <returns>
	/// A new string where each character in <paramref name="inputValue"/> that matches a character in
	/// <paramref name="characters"/> is replaced by the corresponding character in <paramref name="translations"/>.
	/// If no matches are found, the original string is returned unchanged.
	/// </returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the method is invoked directly in application code.
	/// This method is intended for use in LINQ queries with Entity Framework.
	/// </exception>
	string Translate(string inputValue, string characters, string translations);
}
