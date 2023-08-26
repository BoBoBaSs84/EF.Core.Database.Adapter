using System.Globalization;

namespace Domain.Extensions;

/// <summary>
/// The date time extension class.
/// </summary>
public static class DateTimeExtension
{
	/// <summary>
	/// Should cut of the time in date time.
	/// </summary>
	/// <param name="dateTime">The date time to modify.</param>
	/// <returns><see cref="DateTime"/></returns>
	public static DateTime ToSqlDate(this DateTime dateTime) =>
		DateTime.Parse(dateTime.ToShortDateString(), CultureInfo.CurrentCulture);

	/// <summary>
	/// Should cut of the time in date time.
	/// </summary>
	/// <param name="dateTime">The date time to modify.</param>
	/// <returns><see cref="DateTime"/></returns>
	public static DateTime? ToSqlDate(this DateTime? dateTime) =>
		!dateTime.HasValue ? null : DateTime.Parse(dateTime.Value.ToShortDateString(), CultureInfo.CurrentCulture);
}
