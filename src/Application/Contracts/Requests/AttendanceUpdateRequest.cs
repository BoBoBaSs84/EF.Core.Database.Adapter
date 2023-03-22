using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests;

/// <summary>
/// The attendance update request class.
/// </summary>
public sealed class AttendanceUpdadteRequest
{
	/// <summary>
	/// The <see cref="Id"/> property.
	/// </summary>
	public int Id { get; set; } = default!;

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
