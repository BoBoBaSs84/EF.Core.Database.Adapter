using System.ComponentModel.DataAnnotations;

namespace BB84.Home.Application.Contracts.Responses.Common;

/// <summary>
/// The calendar response class.
/// </summary>
public sealed class CalendarResponse
{
	/// <summary>
	/// The date property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime Date { get; set; }

	/// <summary>
	/// The year property.
	/// </summary>
	[Required]
	public int Year { get; set; }

	/// <summary>
	/// The month property.
	/// </summary>
	[Required]
	public int Month { get; set; }

	/// <summary>
	/// The week property.
	/// </summary>
	[Required]
	public int Week { get; set; }

	/// <summary>
	/// The day of week property.
	/// </summary>
	[Required]
	public int DayOfWeek { get; set; }

	/// <summary>
	/// The day of year property.
	/// </summary>
	[Required]
	public int DayOfYear { get; set; }

	/// <summary>
	/// The start of week property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime StartOfWeek { get; set; }

	/// <summary>
	/// The end of Week property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime EndOfWeek { get; set; }

	/// <summary>
	/// The start of month property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime StartOfMonth { get; set; }

	/// <summary>
	/// The end of month property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime EndOfMonth { get; set; }
}
