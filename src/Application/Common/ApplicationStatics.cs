using System.Text.RegularExpressions;

using static Application.Common.ApplicationConstants;

namespace Application.Common;

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
}
