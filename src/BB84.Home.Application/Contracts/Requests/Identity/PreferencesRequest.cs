using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Requests.Identity;

/// <summary>
/// The preferences request class.
/// </summary>
public sealed class PreferencesRequest
{
	/// <summary>
	/// The attendance preferences.
	/// </summary>
	public AttendancePreferencesRequest? AttendancePreferences { get; init; }
}

/// <summary>
/// The attendance preferences request class.
/// </summary>
public sealed class AttendancePreferencesRequest
{
	/// <summary>
	/// The work days per week property.
	/// </summary>
	[Required]
	public required WorkDayTypes WorkDays { get; init; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	[Required, Range(0, 168)]
	public required float WorkHours { get; init; }

	/// <summary>
	/// The vacation days per year.
	/// </summary>
	[Required, Range(0, 365)]
	public required int VacationDays { get; init; }
}
