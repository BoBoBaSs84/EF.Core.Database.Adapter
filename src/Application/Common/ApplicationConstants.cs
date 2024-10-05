namespace Application.Common;

/// <summary>
/// The constants for the application layer.
/// </summary>
internal static class ApplicationConstants
{
	/// <summary>
	/// The date ranges constants class.
	/// </summary>
	internal static class DateRanges
	{
		internal const int MinYear = 1970;
		internal const int MaxYear = 2069;
		internal const int MinMonth = 1;
		internal const int MaxMonth = 12;
		internal const int MinDay = 1;
		internal const int MaxDay = 31;
	}

	/// <summary>
	/// The regex constants class.
	/// </summary>
	internal static class RegexPatterns
	{
		/// <summary>
		/// The email regex pattern.
		/// </summary>
		internal const string Email = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

		/// <summary>
		/// The International Bank Account Number regex pattern.
		/// </summary>
		internal const string IBAN = @"^[a-zA-Z]{2}[0-9]{2}([a-zA-Z0-9]?){16,30}$";

		/// <summary>
		/// A color is represented by three consecutive hexadecimal numbers, each of which represents a color in the RGB color space.
		/// </summary>
		internal const string HEXRGB = @"^#[a-fA-F0-9]{6}$";

		/// <summary>
		/// The permanent account number regex pattern.
		/// </summary>
		/// <remarks> 
		/// Matches Visa, MasterCard, American Express, Diners Club, Discover and JCB cards.
		/// </remarks>
		internal const string PAN = @"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";

		/// <summary>
		/// The bank identification code regex pattern.
		/// </summary>
		internal const string BIC = @"^[A-Z0-9]{4}[A-Z]{2}[A-Z0-9]{2}(?:[A-Z0-9]{3})?$";
	}
}
