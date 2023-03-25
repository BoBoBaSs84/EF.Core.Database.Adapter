using Microsoft.EntityFrameworkCore;
using DbFunction = Domain.Constants.DomainConstants.Sql.DbFunction;

namespace Infrastructure.Persistence;

public sealed partial class ApplicationContext
{
	/// <summary>
	/// Returns a four-character (SOUNDEX) code to evaluate the similarity of two strings.
	/// </summary>
	/// <param name="inputString">Is an alphanumeric expression of character data.</param>
	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = DbFunction.SOUNDLIKE)]
	public static string SoundLike(string inputString)
		=> throw new NotImplementedException();

	/// <summary>
	/// Returns the string provided as a first argument, after some characters specified in the second argument
	/// are translated into a destination set of characters, specified in the third argument.
	/// </summary>
	/// <param name="inputString">The string expression to be searched.</param>
	/// <param name="characters">A string expression containing characters that should be replaced.</param>
	/// <param name="translations">A string expression containing the replacement characters.</param>
	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = DbFunction.TRANSLATE)]
	public static string Translate(string inputString, string characters, string translations)
		=> throw new NotImplementedException();

	/// <summary>
	/// This function returns the last day of the month containing a specified date.
	/// </summary>
	/// <param name="inputDate">A date expression that specifies the date for which to return the last day of the month.</param>
	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = DbFunction.ENDOFMONTH)]
	public static DateTime EndOfMonth(DateTime inputDate)
		=> throw new NotImplementedException();

	/// <summary>
	/// This function returns a Unicode string with the delimiters added to make the
	/// input string a valid SQL Server delimited identifier.
	/// </summary>
	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = DbFunction.QUOTENAME)]
	public static string QuotaName(string inputString)
		=> throw new NotImplementedException();
}
