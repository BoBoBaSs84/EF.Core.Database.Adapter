using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

namespace Application.Contracts.Responses;

/// <summary>
/// The calendar response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ResponseModel"/> class.
/// </remarks>
public sealed class CalendarDayResponse : ResponseModel
{
	/// <summary>
	/// The <see cref="Date"/> property.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime Date { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayType"/> property.
	/// </summary>
	public string DayType { get; set; } = default!;

	/// <summary>
	/// The <see cref="Year"/> property.
	/// </summary>
	public int Year { get; set; } = default!;

	/// <summary>
	/// The <see cref="Month"/> property.
	/// </summary>
	public int Month { get; set; } = default!;

	/// <summary>
	/// The <see cref="Day"/> property.
	/// </summary>
	public int Day { get; set; } = default!;

	/// <summary>
	/// The <see cref="Week"/> property.
	/// </summary>
	public int Week { get; set; } = default!;

	/// <summary>
	/// The <see cref="IsoWeek"/> property.
	/// </summary>
	public int IsoWeek { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayOfYear"/> property.
	/// </summary>
	public int DayOfYear { get; set; } = default!;

	/// <summary>
	/// The <see cref="WeekDay"/> property.
	/// </summary>
	public int WeekDay { get; set; } = default!;

	/// <summary>
	/// The <see cref="EndOfMonth"/> property.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime EndOfMonth { get; set; } = default!;

	/// <summary>
	/// The <see cref="WeekDayName"/> property.
	/// </summary>
	public string WeekDayName { get; set; } = default!;

	/// <summary>
	/// The <see cref="MonthName"/> property.
	/// </summary>
	public string MonthName { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	public int DayTypeId { get; set; } = default!;
}
