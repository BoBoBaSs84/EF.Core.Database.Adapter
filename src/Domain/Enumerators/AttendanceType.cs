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
		Name = nameof(RESX.DayType_Holiday_Name),
		ShortName = nameof(RESX.DayType_Holiday_ShortName),
		Description = nameof(RESX.DayType_Holiday_Description))]
	HOLIDAY = 1,
	/// <summary>
	/// The week day attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Weekday_Name),
		ShortName = nameof(RESX.DayType_Weekday_ShortName),
		Description = nameof(RESX.DayType_Weekday_Description))]
	WEEKDAY,
	/// <summary>
	/// The week end day attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_WeekendDay_Name),
		ShortName = nameof(RESX.DayType_WeekendDay_ShortName),
		Description = nameof(RESX.DayType_WeekendDay_Description))]
	WEEKENDDAY,
	/// <summary>
	/// The work day attendance type.
	/// </summary>	
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Workday_Name),
		ShortName = nameof(RESX.DayType_Workday_ShortName),
		Description = nameof(RESX.DayType_Workday_Description))]
	WORKDAY,
	/// <summary>
	/// The week end work day attendance type.
	/// </summary>	
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_WeekendWorkday_Name),
		ShortName = nameof(RESX.DayType_WeekendWorkday_ShortName),
		Description = nameof(RESX.DayType_WeekendWorkday_Description))]
	[Obsolete("Do not use this one any more, will be removed in the future.")]
	WEEKENDWORKDAY,
	/// <summary>
	/// The absence attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Absence_Name),
		ShortName = nameof(RESX.DayType_Absence_ShortName),
		Description = nameof(RESX.DayType_Absence_Description))]
	ABSENCE,
	/// <summary>
	/// The buisness trip attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_BuisnessTrip_Name),
		ShortName = nameof(RESX.DayType_BuisnessTrip_ShortName),
		Description = nameof(RESX.DayType_BuisnessTrip_Description))]
	BUISNESSTRIP,
	/// <summary>
	/// The suspension attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Suspension_Name),
		ShortName = nameof(RESX.DayType_Suspension_ShortName),
		Description = nameof(RESX.DayType_Suspension_Description))]
	SUSPENSION,
	/// <summary>
	/// The mobile working attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_MobileWorking_Name),
		ShortName = nameof(RESX.DayType_MobileWorking_ShortName),
		Description = nameof(RESX.DayType_MobileWorking_Description))]
	MOBILEWORKING,
	/// <summary>
	/// The planned vacation attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_PlannedVacation_Name),
		ShortName = nameof(RESX.DayType_PlannedVacation_ShortName),
		Description = nameof(RESX.DayType_PlannedVacation_Description))]
	PLANNEDVACATION,
	/// <summary>
	/// The short time work attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_ShortTimeWork_Name),
		ShortName = nameof(RESX.DayType_ShortTimeWork_ShortName),
		Description = nameof(RESX.DayType_ShortTimeWork_Description))]
	SHORTTIMEWORK,
	/// <summary>
	/// The sickness attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Sickness_Name),
		ShortName = nameof(RESX.DayType_Sickness_ShortName),
		Description = nameof(RESX.DayType_Sickness_Description))]
	SICKNESS,
	/// <summary>
	/// The vacation attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Vacation_Name),
		ShortName = nameof(RESX.DayType_Vacation_ShortName),
		Description = nameof(RESX.DayType_Vacation_Description))]
	VACATION,
	/// <summary>
	/// The vacation block attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_VacationBlock_Name),
		ShortName = nameof(RESX.DayType_VacationBlock_ShortName),
		Description = nameof(RESX.DayType_VacationBlock_Description))]
	VACATIONBLOCK
}
