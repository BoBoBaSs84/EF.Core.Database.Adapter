using BB84.EntityFrameworkCore.Entities;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Domain.Entities.Attendance;

/// <summary>
/// The attendance entity class.
/// </summary>
public sealed class AttendanceEntity : AuditedEntity
{
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The <see cref="Date"/> property.
	/// </summary>
	public DateTime Date { get; set; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	public AttendanceType Type { get; set; }

	/// <summary>
	/// The <see cref="StartTime"/> property.
	/// </summary>
	public TimeSpan? StartTime { get; set; }

	/// <summary>
	/// The <see cref="EndTime"/> property.
	/// </summary>
	public TimeSpan? EndTime { get; set; }

	/// <summary>
	/// The <see cref="BreakTime"/> property.
	/// </summary>
	public TimeSpan? BreakTime { get; set; }

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public UserEntity User { get; set; } = default!;
}
