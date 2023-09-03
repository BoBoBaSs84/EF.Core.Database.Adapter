using System.ComponentModel.DataAnnotations;

using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators;

/// <summary>
/// The day type enumerator.
/// </summary>
public enum DayType
{
	/// <summary>
	/// The <see cref="HOLIDAY"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Holiday_Name),
		ShortName = nameof(RESX.DayType_Holiday_ShortName),
		Description = nameof(RESX.DayType_Holiday_Description))]
	HOLIDAY = 1,
	/// <summary>
	/// The <see cref="WEEKDAY"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Weekday_Name),
		ShortName = nameof(RESX.DayType_Weekday_ShortName),
		Description = nameof(RESX.DayType_Weekday_Description))]
	WEEKDAY,
	/// <summary>
	/// The <see cref="WEEKENDDAY"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_WeekendDay_Name),
		ShortName = nameof(RESX.DayType_WeekendDay_ShortName),
		Description = nameof(RESX.DayType_WeekendDay_Description))]
	WEEKENDDAY,
	/// <summary>
	/// The <see cref="WORKDAY"/> day type.
	/// </summary>	
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Workday_Name),
		ShortName = nameof(RESX.DayType_Workday_ShortName),
		Description = nameof(RESX.DayType_Workday_Description))]
	WORKDAY,
	/// <summary>
	/// The <see cref="WEEKENDWORKDAY"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_WeekendWorkday_Name),
		ShortName = nameof(RESX.DayType_WeekendWorkday_ShortName),
		Description = nameof(RESX.DayType_WeekendWorkday_Description))]
	WEEKENDWORKDAY,
	/// <summary>
	/// The <see cref="ABSENCE"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Absence_Name),
		ShortName = nameof(RESX.DayType_Absence_ShortName),
		Description = nameof(RESX.DayType_Absence_Description))]
	ABSENCE,
	/// <summary>
	/// The <see cref="BUISNESSTRIP"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_BuisnessTrip_Name),
		ShortName = nameof(RESX.DayType_BuisnessTrip_ShortName),
		Description = nameof(RESX.DayType_BuisnessTrip_Description))]
	BUISNESSTRIP,
	/// <summary>
	/// The <see cref="SUSPENSION"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Suspension_Name),
		ShortName = nameof(RESX.DayType_Suspension_ShortName),
		Description = nameof(RESX.DayType_Suspension_Description))]
	SUSPENSION,
	/// <summary>
	/// The <see cref="MOBILEWORKING"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_MobileWorking_Name),
		ShortName = nameof(RESX.DayType_MobileWorking_ShortName),
		Description = nameof(RESX.DayType_MobileWorking_Description))]
	MOBILEWORKING,
	/// <summary>
	/// The <see cref="PLANNEDVACATION"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_PlannedVacation_Name),
		ShortName = nameof(RESX.DayType_PlannedVacation_ShortName),
		Description = nameof(RESX.DayType_PlannedVacation_Description))]
	PLANNEDVACATION,
	/// <summary>
	/// The <see cref="SHORTTIMEWORK"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_ShortTimeWork_Name),
		ShortName = nameof(RESX.DayType_ShortTimeWork_ShortName),
		Description = nameof(RESX.DayType_ShortTimeWork_Description))]
	SHORTTIMEWORK,
	/// <summary>
	/// The <see cref="SICKNESS"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Sickness_Name),
		ShortName = nameof(RESX.DayType_Sickness_ShortName),
		Description = nameof(RESX.DayType_Sickness_Description))]
	SICKNESS,
	/// <summary>
	/// The <see cref="VACATION"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_Vacation_Name),
		ShortName = nameof(RESX.DayType_Vacation_ShortName),
		Description = nameof(RESX.DayType_Vacation_Description))]
	VACATION,
	/// <summary>
	/// The <see cref="VACATIONBLOCK"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DayType_VacationBlock_Name),
		ShortName = nameof(RESX.DayType_VacationBlock_ShortName),
		Description = nameof(RESX.DayType_VacationBlock_Description))]
	VACATIONBLOCK
}
