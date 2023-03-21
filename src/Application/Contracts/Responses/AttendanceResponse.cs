using Application.Contracts.Responses.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Responses;

/// <summary>
/// The attendance response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ResponseModel"/> class.
/// </remarks>
public sealed class AttendanceResponse : ResponseModel
{
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>	
	public int UserId { get; set; } = default!;

	/// <summary>
	/// The <see cref="CalendarDayId"/> property.
	/// </summary>
	public int CalendarDayId { get; set; } = default!;

	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	public int DayTypeId { get; set; } = default!;

	/// <summary>
	/// The <see cref="StartTime"/> property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? StartTime { get; set; } = default!;

	/// <summary>
	/// The <see cref="EndTime"/> property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? EndTime { get; set; } = default!;

	/// <summary>
	/// The <see cref="BreakTime"/> property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? BreakTime { get; set; } = default!;
}
