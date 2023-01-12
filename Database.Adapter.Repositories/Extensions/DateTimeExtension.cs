using System.Globalization;

namespace Database.Adapter.Repositories.Extensions;

internal static class DateTimeExtension
{
	private static readonly CultureInfo cultureInfo = CultureInfo.CurrentCulture;

	public static DateTime ToSqlDate(this DateTime dateTime) =>
		DateTime.Parse(dateTime.ToShortDateString(), cultureInfo);
}
