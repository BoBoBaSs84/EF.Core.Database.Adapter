using Domain.Enumerators;

namespace Application.Contracts.Requests.Attendance;

/// <summary>
/// The attendance settings request class.
/// </summary>
/// <remarks>
/// This request is used for creation and update.
/// </remarks>
public sealed class AttendanceSettingsRequest
{
	/// <summary>
	/// The work days per week.
	/// </summary>
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work hours per week.
	/// </summary>
	public float WorkHours { get; set; }
}
