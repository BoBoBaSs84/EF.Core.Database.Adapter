using System.Text.RegularExpressions;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Domain;

/// <summary>
/// The statics class.
/// </summary>
public static class Statics
{
	/// <summary>
	/// The <see cref="CreditCardRegex"/> property.
	/// </summary>
	public static Regex CreditCardRegex { get; } = new(RegexPatterns.CC);

	/// <summary>
	/// The <see cref="EmailRegex"/> property.
	/// </summary>
	public static Regex EmailRegex { get; } = new(RegexPatterns.Email);

	/// <summary>
	/// The <see cref="IbanRegex"/> property.
	/// </summary>
	public static Regex IbanRegex { get; } = new(RegexPatterns.IBAN);

	/// <summary>
	/// The <see cref="WhitespaceRegex"/> property.
	/// </summary>
	public static Regex WhitespaceRegex { get; } = new(RegexPatterns.Whitespace);
}
