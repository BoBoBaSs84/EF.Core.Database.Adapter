using System.ComponentModel.DataAnnotations;
using Database.Adapter.Entities.Properties;
using static Database.Adapter.Entities.Properties.Resources;

namespace Database.Adapter.Entities.Enumerators;

/// <summary>
/// The day type enumerator.
/// </summary>
public enum DayType
{
	/// <summary>
	/// The <see cref="Absence"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_Absence_Name),
		ShortName = nameof(Enumerator_DayType_Absence_ShortName),
		Description = nameof(Enumerator_DayType_Absence_Description))]
	Absence,
	/// <summary>
	/// The <see cref="BuisnessTrip"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_BuisnessTrip_Name),
		ShortName = nameof(Enumerator_DayType_BuisnessTrip_ShortName),
		Description = nameof(Enumerator_DayType_BuisnessTrip_Description))]
	BuisnessTrip,
	/// <summary>
	/// The <see cref="Suspension"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_Suspension_Name),
		ShortName = nameof(Enumerator_DayType_Suspension_ShortName),
		Description = nameof(Enumerator_DayType_Suspension_Description))]
	Suspension,
	/// <summary>
	/// The <see cref="Holiday"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_Holiday_Name),
		ShortName = nameof(Enumerator_DayType_Holiday_ShortName),
		Description = nameof(Enumerator_DayType_Holiday_Description))]
	Holiday,
	/// <summary>
	/// The <see cref="MobileWorking"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_MobileWorking_Name),
		ShortName = nameof(Enumerator_DayType_MobileWorking_ShortName),
		Description = nameof(Enumerator_DayType_MobileWorking_Description))]
	MobileWorking,
	/// <summary>
	/// The <see cref="PlannedVacation"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_PlannedVacation_Name),
		ShortName = nameof(Enumerator_DayType_PlannedVacation_ShortName),
		Description = nameof(Enumerator_DayType_PlannedVacation_Description))]
	PlannedVacation,
	/// <summary>
	/// The <see cref="BusinessDay"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_BusinessDay_Name),
		ShortName = nameof(Enumerator_DayType_BusinessDay_ShortName),
		Description = nameof(Enumerator_DayType_BusinessDay_Description))]
	BusinessDay,
	/// <summary>
	/// The <see cref="ShortTimeWork"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_ShortTimeWork_Name),
		ShortName = nameof(Enumerator_DayType_ShortTimeWork_ShortName),
		Description = nameof(Enumerator_DayType_ShortTimeWork_Description))]
	ShortTimeWork,
	/// <summary>
	/// The <see cref="Sickness"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_Sickness_Name),
		ShortName = nameof(Enumerator_DayType_Sickness_ShortName),
		Description = nameof(Enumerator_DayType_Sickness_Description))]
	Sickness,
	/// <summary>
	/// The <see cref="Vacation"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_Vacation_Name),
		ShortName = nameof(Enumerator_DayType_Vacation_ShortName),
		Description = nameof(Enumerator_DayType_Vacation_Description))]
	Vacation,
	/// <summary>
	/// The <see cref="VacationBlock"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_VacationBlock_Name),
		ShortName = nameof(Enumerator_DayType_VacationBlock_ShortName),
		Description = nameof(Enumerator_DayType_VacationBlock_Description))]
	VacationBlock,
	/// <summary>
	/// The <see cref="Weekend"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_Weekend_Name),
		ShortName = nameof(Enumerator_DayType_Weekend_ShortName),
		Description = nameof(Enumerator_DayType_Weekend_Description))]
	Weekend,
	/// <summary>
	/// The <see cref="WeekendWorkday"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_DayType_WeekendWorkday_Name),
		ShortName = nameof(Enumerator_DayType_WeekendWorkday_ShortName),
		Description = nameof(Enumerator_DayType_WeekendWorkday_Description))]
	WeekendWorkday
}
