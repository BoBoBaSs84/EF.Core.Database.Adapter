using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class MasterContext
{
	/// <summary>
	/// This method is a dummy method for calling a sql server scalar valued function.
	/// </summary>
	/// <param name="cultureName">The culture name, like "en-US".</param>
	/// <returns>The language code identifier.</returns>
	/// <exception cref="NotSupportedException"></exception>
	[SuppressMessage("Style", "IDE0060", Justification = "This has to be this way.")]
	public int GetLangageCodeIdentifier(string cultureName) =>
		throw new NotSupportedException();
}
