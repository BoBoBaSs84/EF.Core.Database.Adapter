using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Domain.Extensions;

/// <summary>
/// The attendance type extensions class.
/// </summary>
public static class AttendanceTypeExtensions
{
	/// <summary>
	/// Returns if the attendance type is working hours relevant.
	/// </summary>
	/// <param name="type">The attendance type enumerator to work with.</param>
	/// <returns>
	/// <see langword="true"/> if the attendance type is working hours relevant, otherwise <see langword="false"/>
	/// </returns>
	public static bool IsWorkingHoursRelevant(this AttendanceType type)
		=> type switch
		{
			AttendanceType.Workday => true,
			AttendanceType.Absence => true,
			AttendanceType.BuisnessTrip => true,
			AttendanceType.MobileWorking => true,
			AttendanceType.ShortTimeWork => true,
			AttendanceType.VacationBlock => true,
			AttendanceType.PlannedVacation => true,
			_ => false,
		};
}
