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
	public DateTime Date { get; set; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	[Required]
	public AttendanceType Type { get; set; }

	/// <summary>
	/// The start time property.
	/// </summary>	
	[DataType(DataType.Time)]
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The end time property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The break time property.
	/// </summary>
	[DataType(DataType.Time)]
	public TimeSpan? BreakTime { get; set; }

	/// <summary>
	/// The resulting working hours property.
	/// </summary>
	public float? WorkingHours { get; set; }
}
