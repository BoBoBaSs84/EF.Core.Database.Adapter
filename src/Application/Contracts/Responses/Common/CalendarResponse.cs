using System.Text.Json.Serialization;

using Domain.Converters;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The calendar response class.
/// </summary>
public sealed class CalendarResponse
{
	/// <summary>
	/// The date property.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime Date { get; set; }

	/// <summary>
	/// The year property.
	/// </summary>
	public int Year { get; set; }

	/// <summary>
	/// The month property.
	/// </summary>
	public int Month { get; set; }

	/// <summary>
	/// The week property.
	/// </summary>
	public int Week { get; set; }

	/// <summary>
	/// The day of week property.
	/// </summary>
	public int DayOfWeek { get; set; }

	/// <summary>
	/// The day of year property.
	/// </summary>
	public int DayOfYear { get; set; }

	/// <summary>
	/// The start of week property.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime StartOfWeek { get; set; }

	/// <summary>
	/// The end of Week property.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime EndOfWeek { get; set; }

	/// <summary>
	/// The start of month property.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime StartOfMonth { get; set; }

	/// <summary>
	/// The end of month property.
	/// </summary>
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime EndOfMonth { get; set; }
}
