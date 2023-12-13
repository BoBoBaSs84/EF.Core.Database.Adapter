using System.ComponentModel.DataAnnotations;

using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators;

/// <summary>
/// The attendance type enumerator.
/// </summary>
public enum AttendanceType
{
	/// <summary>
	/// The holiday attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Holiday_Name),
		ShortName = nameof(RESX.AttendanceType_Holiday_ShortName),
		Description = nameof(RESX.AttendanceType_Holiday_Description))]
	HOLIDAY = 1,
	/// <summary>
	/// The work day attendance type.
	/// </summary>	
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Workday_Name),
		ShortName = nameof(RESX.AttendanceType_Workday_ShortName),
		Description = nameof(RESX.AttendanceType_Workday_Description))]
	WORKDAY,
	/// <summary>
	/// The absence attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Absence_Name),
		ShortName = nameof(RESX.AttendanceType_Absence_ShortName),
		Description = nameof(RESX.AttendanceType_Absence_Description))]
	ABSENCE,
	/// <summary>
	/// The buisness trip attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_BuisnessTrip_Name),
		ShortName = nameof(RESX.AttendanceType_BuisnessTrip_ShortName),
		Description = nameof(RESX.AttendanceType_BuisnessTrip_Description))]
	BUISNESSTRIP,
	/// <summary>
	/// The suspension attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Suspension_Name),
		ShortName = nameof(RESX.AttendanceType_Suspension_ShortName),
		Description = nameof(RESX.AttendanceType_Suspension_Description))]
	SUSPENSION,
	/// <summary>
	/// The mobile working attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_MobileWorking_Name),
		ShortName = nameof(RESX.AttendanceType_MobileWorking_ShortName),
		Description = nameof(RESX.AttendanceType_MobileWorking_Description))]
	MOBILEWORKING,
	/// <summary>
	/// The planned vacation attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_PlannedVacation_Name),
		ShortName = nameof(RESX.AttendanceType_PlannedVacation_ShortName),
		Description = nameof(RESX.AttendanceType_PlannedVacation_Description))]
	PLANNEDVACATION,
	/// <summary>
	/// The short time work attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_ShortTimeWork_Name),
		ShortName = nameof(RESX.AttendanceType_ShortTimeWork_ShortName),
		Description = nameof(RESX.AttendanceType_ShortTimeWork_Description))]
	SHORTTIMEWORK,
	/// <summary>
	/// The sickness attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Sickness_Name),
		ShortName = nameof(RESX.AttendanceType_Sickness_ShortName),
		Description = nameof(RESX.AttendanceType_Sickness_Description))]
	SICKNESS,
	/// <summary>
	/// The vacation attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Vacation_Name),
		ShortName = nameof(RESX.AttendanceType_Vacation_ShortName),
		Description = nameof(RESX.AttendanceType_Vacation_Description))]
	VACATION,
	/// <summary>
	/// The vacation block attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_VacationBlock_Name),
		ShortName = nameof(RESX.AttendanceType_VacationBlock_ShortName),
		Description = nameof(RESX.AttendanceType_VacationBlock_Description))]
	VACATIONBLOCK
}
