using System.Globalization;

namespace Domain.Extensions;

/// <summary>
/// The date time extension class.
/// </summary>
public static class DateTimeExtension
{
	/// <summary>
	/// Returns the ISO 8601 week from the provided date. 
	/// </summary>
	/// <param name="dateTime">The date time value to work with.</param>
	/// <returns>The iso week.</returns>
	public static int WeekOfYear(this DateTime dateTime)
		=> ISOWeek.GetWeekOfYear(dateTime);
}
