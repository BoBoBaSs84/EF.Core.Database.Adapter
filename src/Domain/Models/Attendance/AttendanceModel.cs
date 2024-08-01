using BB84.EntityFrameworkCore.Models;

using Domain.Enumerators;
using Domain.Models.Identity;

namespace Domain.Models.Attendance;

/// <summary>
/// The attendance entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
public sealed class AttendanceModel : AuditedModel
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
	public AttendanceType AttendanceType { get; set; }

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
	public UserModel User { get; set; } = default!;
}
