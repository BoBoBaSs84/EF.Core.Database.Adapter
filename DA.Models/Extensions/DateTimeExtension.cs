using static DA.Models.Statics;

namespace DA.Models.Extensions;

/// <summary>
/// The date time extension class.
/// </summary>
public static class DateTimeExtension
{
	/// <summary>
	/// Should cut of the time in date time.
	/// </summary>
	/// <param name="dateTime">The date time to modify.</param>
	/// <returns></returns>
	public static DateTime ToSqlDate(this DateTime dateTime) =>
		DateTime.Parse(dateTime.ToShortDateString(), CurrentCulture);
}
