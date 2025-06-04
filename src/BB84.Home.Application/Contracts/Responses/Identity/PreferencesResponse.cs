using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using BB84.Home.Application.Converters;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Responses.Identity;

/// <summary>
/// The preferences response class.
/// </summary>
public sealed class PreferencesResponse
{
	/// <summary>
	/// The attendance preferences.
	/// </summary>
	public AttendancePreferencesResponse? AttendancePreferences { get; init; }
}

/// <summary>
/// The attendance preferences response class.
/// </summary>
public sealed class AttendancePreferencesResponse
{
	/// <summary>
	/// The work days per week property.
	/// </summary>
	[Required]
	[JsonConverter(typeof(FlagsJsonConverterFactory))]
	public required WorkDayTypes WorkDays { get; init; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	[Required]
	public required float WorkHours { get; init; }

	/// <summary>
	/// The vacation days per year.
	/// </summary>
	[Required]
	public required int VacationDays { get; init; }
}
