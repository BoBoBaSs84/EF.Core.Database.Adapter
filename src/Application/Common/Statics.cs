using CDR = Application.Common.Constants.DateRanges;

namespace Application.Common;

/// <summary>
/// The Application statics class.
/// </summary>
internal static class Statics
{
	/// <summary>
	/// The sate ranges statics class.
	/// </summary>
	public static class DateRanges
	{
		public static readonly DateTime MinDate = new(CDR.MinYear, CDR.MinMonth, CDR.MinDay);
		public static readonly DateTime MaxDate = new(CDR.MaxYear, CDR.MaxMonth, CDR.MaxDay);
	}
}
