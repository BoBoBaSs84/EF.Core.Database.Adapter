using Microsoft.EntityFrameworkCore;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence;

internal sealed partial class RepositoryContext
{
	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = SqlDbFunction.DIFFERENCE)]
	public int Difference(string inputValue, string valueToCompare)
		=> throw new InvalidOperationException("Method can not be used like this.");

	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = SqlDbFunction.SOUNDEX)]
	public string Soundex(string inputValue)
		=> throw new InvalidOperationException("Method can not be used like this.");

	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = SqlDbFunction.TRANSLATE)]
	public string Translate(string inputValue, string characters, string translations)
		=> throw new InvalidOperationException("Method can not be used like this.");

	[DbFunction(IsBuiltIn = true, IsNullable = false, Name = SqlDbFunction.ENDOFMONTH)]
	public DateTime EndOfMonth(DateTime inputDate)
		=> throw new InvalidOperationException("Method can not be used like this.");

	[DbFunction(IsBuiltIn = true, IsNullable = true, Name = SqlDbFunction.QUOTENAME)]
	public string? QuoteName(string inputValue)
		=> throw new InvalidOperationException("Method can not be used like this.");
}
