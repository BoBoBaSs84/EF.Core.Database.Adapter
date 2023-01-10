using Microsoft.EntityFrameworkCore;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class MasterContext
{
	/// <summary>
	/// Method should return the  langage code identifier.
	/// </summary>
	/// <param name="cultureName">The culture name, like "en-US"</param>
	/// <returns>The langage code identifier</returns>
	/// <exception cref="NotImplementedException"></exception>
	[DbFunction(Name = nameof(GetLangageCodeIdentifier), Schema = SqlSchema.CLR, IsBuiltIn = false)]
	public static int GetLangageCodeIdentifier(string cultureName) =>
		throw new NotImplementedException($"Parameter: {cultureName}");
}
