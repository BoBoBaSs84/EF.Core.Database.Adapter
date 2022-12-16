using System.ComponentModel.DataAnnotations;

namespace Database.Adapter.Entities.Enumerators;

/// <summary>
/// The day type enumerator.
/// </summary>
public enum DayType
{
	/// <summary>
	/// The <see cref="Absence"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Absence",
		ShortName = "AB",
		Description = "")]
	Absence,
	/// <summary>
	/// The <see cref="BuisnessTrip"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Buisness trip",
		ShortName = "BT",
		Description = "")]
	BuisnessTrip,
	/// <summary>
	/// The <see cref="Exemption"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Exemption",
		ShortName = "EX",
		Description = "")]
	Exemption,
	/// <summary>
	/// The <see cref="Holiday"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Holiday",
		ShortName = "HD",
		Description = "")]
	Holiday,
	/// <summary>
	/// The <see cref="MobileWorking"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Mobile working",
		ShortName = "MW",
		Description = "")]
	MobileWorking,
	/// <summary>
	/// The <see cref="PlannedVacation"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Planned vacation",
		ShortName = "PV",
		Description = "")]
	PlannedVacation,
	/// <summary>
	/// The <see cref="RegularWorkday"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Regular workday",
		ShortName = "RW",
		Description = "")]
	RegularWorkday,
	/// <summary>
	/// The <see cref="ShortTimeWork"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Short time work",
		ShortName = "ST",
		Description = "")]
	ShortTimeWork,
	/// <summary>
	/// The <see cref="Sickness"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Sickness",
		ShortName = "SN",
		Description = "")]
	Sickness,
	/// <summary>
	/// The <see cref="Vacation"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Vacation",
		ShortName = "VC",
		Description = "")]
	Vacation,
	/// <summary>
	/// The <see cref="VacationLock"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Vacation lock",
		ShortName = "VL",
		Description = "")]
	VacationLock,
	/// <summary>
	/// The <see cref="Weekend"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Weekend",
		ShortName = "WK",
		Description = "")]
	Weekend,
	/// <summary>
	/// The <see cref="WeekendWorkday"/> day type.
	/// </summary>
	[Display(ResourceType = typeof(Properties.Resources),
		Name = "Weekend workday",
		ShortName = "WW",
		Description = "")]
	WeekendWorkday
}
