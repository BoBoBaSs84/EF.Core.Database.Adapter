using Domain.Enumerators.Attendance;

namespace Domain.Extensions;

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
			AttendanceType.WORKDAY => true,
			AttendanceType.ABSENCE => true,
			AttendanceType.BUISNESSTRIP => true,
			AttendanceType.MOBILEWORKING => true,
			AttendanceType.SHORTTIMEWORK => true,
			AttendanceType.VACATIONBLOCK => true,
			AttendanceType.PLANNEDVACATION => true,
			_ => false,
		};
}
