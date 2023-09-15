﻿using System.Globalization;

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
	public static int GetIsoWeek(this DateTime dateTime)
		=> ISOWeek.GetWeekOfYear(dateTime);

	/// <summary>
	/// Removes the time in date time.
	/// </summary>
	/// <param name="dateTime">The date time to modify.</param>
	/// <returns><see cref="DateTime"/></returns>
	public static DateTime ToSqlDate(this DateTime dateTime)
		=> new(dateTime.Year, dateTime.Month, dateTime.Day);

	/// <summary>
	/// Removes the time in date time.
	/// </summary>
	/// <param name="dateTime">The date time to modify.</param>
	/// <returns><see cref="DateTime"/></returns>
	public static DateTime? ToSqlDate(this DateTime? dateTime)
		=> !dateTime.HasValue ? null : new(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);

	/// <summary>
	/// Calculates the first day of the week using the specified date and
	/// the definition of the first day of the week.
	/// </summary>
	/// <param name="dateTime">The date time value to work with.</param>
	/// <param name="startOfWeek">The first day of the week.</param>
	/// <returns>The date of the first day of the week.</returns>
	public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek = DayOfWeek.Monday)
		=> dateTime.AddDays(-1 * (7 + (dateTime.DayOfWeek - startOfWeek)) % 7).Date;

	/// <summary>
	/// Calculates the last day of the week using the specified date and
	/// the definition of the first day of the week.
	/// </summary>
	/// <param name="dateTime">The date time value to work with.</param>
	/// <param name="startOfWeek">The first day of the week.</param>
	/// <returns>The date of the last day of the week.</returns>
	public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek startOfWeek = DayOfWeek.Monday)
		=> StartOfWeek(dateTime, startOfWeek).AddDays(6);

	/// <summary>
	/// Calculates the first day of the month using the specified date.
	/// </summary>
	/// <param name="dateTime">The date time value to work with.</param>
	/// <returns>The date of the first day of the month.</returns>
	public static DateTime StartOfMonth(this DateTime dateTime)
		=> new(dateTime.Year, dateTime.Month, 1);

	/// <summary>
	/// Calculates the last day of the month using the specified date.
	/// </summary>
	/// <param name="dateTime">The date time value to work with.</param>
	/// <returns>The date of the last day of the month.</returns>
	public static DateTime EndOfMonth(this DateTime dateTime)
		=> StartOfMonth(dateTime).AddMonths(1).AddDays(-1);
}
