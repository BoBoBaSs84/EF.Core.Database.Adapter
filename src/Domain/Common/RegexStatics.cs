using System.Text.RegularExpressions;

using RegexPatterns = Domain.Common.Constants.RegexPatterns;

namespace Domain.Common;

/// <summary>
/// The statics class.
/// </summary>
public static partial class RegexStatics
{
	/// <summary>
	/// The <see cref="PAN"/> property.
	/// </summary>
	public static Regex PAN { get; } = PANRegex();

	/// <summary>
	/// The <see cref="Email"/> property.
	/// </summary>
	public static Regex Email { get; } = EmailRegex();

	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>
	public static Regex IBAN { get; } = IBANRegex();

	/// <summary>
	/// The <see cref="Whitespace"/> property.
	/// </summary>
	public static Regex Whitespace { get; } = WhitespaceRegex();

	[GeneratedRegex(RegexPatterns.PAN)]
	private static partial Regex PANRegex();
	
	[GeneratedRegex(RegexPatterns.Email)]
	private static partial Regex EmailRegex();
	
	[GeneratedRegex(RegexPatterns.IBAN)]
	private static partial Regex IBANRegex();
	
	[GeneratedRegex(RegexPatterns.Whitespace)]
	private static partial Regex WhitespaceRegex();
}
