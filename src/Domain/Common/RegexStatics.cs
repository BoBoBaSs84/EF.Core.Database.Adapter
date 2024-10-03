using System.Text.RegularExpressions;

using RegexPatterns = Domain.Common.Constants.RegexPatterns;

namespace Domain.Common;

/// <summary>
/// The statics class.
/// </summary>
public static partial class RegexStatics
{
	/// <summary>
	/// The permanent account number regex.
	/// </summary>
	public static Regex PAN { get; } = PANRegex();

	/// <summary>
	/// The international bank account number regex.
	/// </summary>
	public static Regex IBAN { get; } = IBANRegex();

	/// <summary>
	/// The bank identification code regex.
	/// </summary>
	public static Regex BIC { get; } = BICRegex();

	[GeneratedRegex(RegexPatterns.PAN)]
	private static partial Regex PANRegex();

	[GeneratedRegex(RegexPatterns.IBAN)]
	private static partial Regex IBANRegex();

	[GeneratedRegex(RegexPatterns.BIC)]
	private static partial Regex BICRegex();
}
