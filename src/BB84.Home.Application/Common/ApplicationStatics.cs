using System.Text.RegularExpressions;

using static BB84.Home.Application.Common.ApplicationConstants;

namespace BB84.Home.Application.Common;

/// <summary>
/// The Application statics class.
/// </summary>
public static partial class ApplicationStatics
{
	/// <summary>
	/// The sate ranges statics class.
	/// </summary>
	public static class DateStatics
	{
		/// <summary>
		/// The smallest date to be processed.
		/// </summary>
		public static readonly DateTime MinDate = new(DateRanges.MinYear, DateRanges.MinMonth, DateRanges.MinDay);

		/// <summary>
		/// The largest date to be processed.
		/// </summary>
		public static readonly DateTime MaxDate = new(DateRanges.MaxYear, DateRanges.MaxMonth, DateRanges.MaxDay);
	}

	/// <summary>
	/// The statics class.
	/// </summary>
	public static partial class RegexStatics
	{
		/// <summary>
		/// The permanent account number regex.
		/// </summary>
		public static Regex PAN { get; } = PanRegex();

		/// <summary>
		/// The international bank account number regex.
		/// </summary>
		public static Regex IBAN { get; } = IbanRegex();

		/// <summary>
		/// The bank identification code regex.
		/// </summary>
		public static Regex BIC { get; } = BicRegex();

		/// <summary>
		/// The rgb hex regex.
		/// </summary>
		public static Regex HEXRGB { get; } = HexRgbRegex();

		[GeneratedRegex(RegexPatterns.PAN)]
		private static partial Regex PanRegex();

		[GeneratedRegex(RegexPatterns.IBAN)]
		private static partial Regex IbanRegex();

		[GeneratedRegex(RegexPatterns.BIC)]
		private static partial Regex BicRegex();

		[GeneratedRegex(RegexPatterns.HEXRGB)]
		private static partial Regex HexRgbRegex();
	}
}
