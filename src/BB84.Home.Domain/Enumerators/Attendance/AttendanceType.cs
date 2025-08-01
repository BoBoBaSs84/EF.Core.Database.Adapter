﻿using System.ComponentModel.DataAnnotations;

using RESX = BB84.Home.Domain.Properties.EnumeratorResources;

namespace BB84.Home.Domain.Enumerators.Attendance;

/// <summary>
/// Represents the various types of attendance that can be assigned to an individual.
/// </summary>
/// <remarks>
/// This enumeration defines distinct attendance types, such as holidays, workdays, absences, and others.
/// Each attendance type is associated with metadata for display purposes, including localized names, short
/// names, and descriptions. These metadata values are retrieved from the specified resource file.
/// </remarks>
public enum AttendanceType : byte
{
	/// <summary>
	/// The holiday attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Holiday_Name),
		ShortName = nameof(RESX.AttendanceType_Holiday_ShortName),
		Description = nameof(RESX.AttendanceType_Holiday_Description))]
	Holiday = 1,
	/// <summary>
	/// The work day attendance type.
	/// </summary>	
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Workday_Name),
		ShortName = nameof(RESX.AttendanceType_Workday_ShortName),
		Description = nameof(RESX.AttendanceType_Workday_Description))]
	Workday,
	/// <summary>
	/// The absence attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Absence_Name),
		ShortName = nameof(RESX.AttendanceType_Absence_ShortName),
		Description = nameof(RESX.AttendanceType_Absence_Description))]
	Absence,
	/// <summary>
	/// The buisness trip attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_BuisnessTrip_Name),
		ShortName = nameof(RESX.AttendanceType_BuisnessTrip_ShortName),
		Description = nameof(RESX.AttendanceType_BuisnessTrip_Description))]
	BuisnessTrip,
	/// <summary>
	/// The suspension attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Suspension_Name),
		ShortName = nameof(RESX.AttendanceType_Suspension_ShortName),
		Description = nameof(RESX.AttendanceType_Suspension_Description))]
	Suspension,
	/// <summary>
	/// The mobile working attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_MobileWorking_Name),
		ShortName = nameof(RESX.AttendanceType_MobileWorking_ShortName),
		Description = nameof(RESX.AttendanceType_MobileWorking_Description))]
	MobileWorking,
	/// <summary>
	/// The planned vacation attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_PlannedVacation_Name),
		ShortName = nameof(RESX.AttendanceType_PlannedVacation_ShortName),
		Description = nameof(RESX.AttendanceType_PlannedVacation_Description))]
	PlannedVacation,
	/// <summary>
	/// The short time work attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_ShortTimeWork_Name),
		ShortName = nameof(RESX.AttendanceType_ShortTimeWork_ShortName),
		Description = nameof(RESX.AttendanceType_ShortTimeWork_Description))]
	ShortTimeWork,
	/// <summary>
	/// The sickness attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Sickness_Name),
		ShortName = nameof(RESX.AttendanceType_Sickness_ShortName),
		Description = nameof(RESX.AttendanceType_Sickness_Description))]
	Sickness,
	/// <summary>
	/// The vacation attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Vacation_Name),
		ShortName = nameof(RESX.AttendanceType_Vacation_ShortName),
		Description = nameof(RESX.AttendanceType_Vacation_Description))]
	Vacation,
	/// <summary>
	/// The vacation block attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_VacationBlock_Name),
		ShortName = nameof(RESX.AttendanceType_VacationBlock_ShortName),
		Description = nameof(RESX.AttendanceType_VacationBlock_Description))]
	VacationBlock,
	/// <summary>
	/// The training attendance type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AttendanceType_Training_Name),
		ShortName = nameof(RESX.AttendanceType_Training_ShortName),
		Description = nameof(RESX.AttendanceType_Training_Description))]
	Training
}
