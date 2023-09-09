using Domain.Enumerators;
using Domain.Models.Base;

namespace Domain.Models.Attendance;

/// <summary>
/// The user setting model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class AttendanceSettingsModel : AuditedModel
{
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The work days per week property.
	/// </summary>
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	public float WorkHours { get; set; }
}
