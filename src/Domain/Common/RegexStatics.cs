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
	/// The International Bank Account Number regex.
	/// </summary>
	public static Regex IBAN { get; } = IBANRegex();

	[GeneratedRegex(RegexPatterns.PAN)]
	private static partial Regex PANRegex();

	[GeneratedRegex(RegexPatterns.IBAN)]
	private static partial Regex IBANRegex();
}
