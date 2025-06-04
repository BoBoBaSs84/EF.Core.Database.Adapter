using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Responses.Attendance;

/// <summary>
/// The attendance response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class AttendanceResponse : IdentityResponse
{
	/// <summary>
	/// The date property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public required DateTime Date { get; init; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	[Required]
	public required AttendanceType Type { get; init; }

	/// <summary>
	/// The start time property.
	/// </summary>	
	[DataType(DataType.Time)]
	public TimeSpan? StartTime { get; init; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? EndTime { get; init; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? BreakTime { get; init; }

	/// <summary>
	/// The resulting working hours property.
	/// </summary>
	public float? WorkingHours { get; init; }
}
