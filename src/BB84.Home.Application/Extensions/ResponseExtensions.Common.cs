using System.Globalization;

using BB84.Home.Application.Contracts.Responses.Common;

namespace BB84.Home.Application.Extensions;

internal static partial class ResponseExtensions
{
	/// <summary>
	/// Converts a DateTime to a CalendarResponse, which contains various calendar-related information
	/// about the date, such as the year, month, week of year, day of week, day of year, start and end
	/// of the week, and start and end of the month.
	/// </summary>
	/// <param name="date">The DateTime to convert to a CalendarResponse.</param>
	/// <returns>The CalendarResponse containing calendar-related information about the input date.</returns>
	public static CalendarResponse ToResponse(this DateTime date)
	{
		// compute week of year using ISO style: Monday as first day
		Calendar calendar = CultureInfo.InvariantCulture.Calendar;
		int week = calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

		int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
		DateTime startOfWeek = date.Date.AddDays(-diff);
		DateTime endOfWeek = startOfWeek.AddDays(6);

		CalendarResponse response = new()
		{
			Date = date,
			Year = date.Year,
			Month = date.Month,
			Week = week,
			DayOfWeek = (int)date.DayOfWeek,
			DayOfYear = date.DayOfYear,
			StartOfWeek = startOfWeek,
			EndOfWeek = endOfWeek,
			StartOfMonth = new DateTime(date.Year, date.Month, 1),
			EndOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
		};

		return response;
	}
}
