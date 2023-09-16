using Domain.Enumerators;

namespace Application.Contracts.Requests.Identity;

/// <summary>
/// The preferences request class.
/// </summary>
public sealed class PreferencesRequest
{
	/// <summary>
	/// The attendance preferences.
	/// </summary>
	public AttendancePreferencesRequest? AttendancePreferences { get; set; }
}

/// <summary>
/// The attendance preferences request class.
/// </summary>
public sealed class AttendancePreferencesRequest
{
	/// <summary>
	/// The work days per week property.
	/// </summary>
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	public float WorkHours { get; set; }
}
