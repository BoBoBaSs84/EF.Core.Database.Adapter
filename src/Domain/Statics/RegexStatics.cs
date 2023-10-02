using System.Text.RegularExpressions;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Domain.Statics;

/// <summary>
/// The statics class.
/// </summary>
public static class RegexStatics
{
	/// <summary>
	/// The <see cref="CreditCard"/> property.
	/// </summary>
	public static Regex CreditCard { get; } = new(RegexPatterns.PAN);

	/// <summary>
	/// The <see cref="Email"/> property.
	/// </summary>
	public static Regex Email { get; } = new(RegexPatterns.Email);

	/// <summary>
	/// The <see cref="Iban"/> property.
	/// </summary>
	public static Regex Iban { get; } = new(RegexPatterns.IBAN);

	/// <summary>
	/// The <see cref="Whitespace"/> property.
	/// </summary>
	public static Regex Whitespace { get; } = new(RegexPatterns.Whitespace);
}
