﻿using System.ComponentModel.DataAnnotations;

using RESX = BB84.Home.Domain.Properties.EnumeratorResources;

namespace BB84.Home.Domain.Enumerators.Attendance;

/// <summary>
/// Represents the days of the week that can be designated as work days.
/// </summary>
/// <remarks>
/// This enumeration is marked with the <see cref="FlagsAttribute"/>, allowing bitwise operations
/// to combine multiple values. Each value corresponds to a specific day of the week.
/// </remarks>
[Flags]
public enum WorkDayTypes : byte
{
	/// <summary>
	/// Represents the absence of a specific value or state.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_None_Name),
		ShortName = nameof(RESX.WorkDayTypes_None_ShortName),
		Description = nameof(RESX.WorkDayTypes_None_Description))]
	None = 0,
	/// <summary>
	/// Indicates Monday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Monday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Monday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Monday_Description))]
	Monday = 1 << 0,
	/// <summary>
	/// Indicates Tuesday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Tuesday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Tuesday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Tuesday_Description))]
	Tuesday = 1 << 1,
	/// <summary>
	/// Indicates Wednesday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Wednesday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Wednesday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Wednesday_Description))]
	Wednesday = 1 << 2,
	/// <summary>
	/// Indicates Thursday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Thursday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Thursday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Thursday_Description))]
	Thursday = 1 << 3,
	/// <summary>
	/// Indicates Friday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Friday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Friday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Friday_Description))]
	Friday = 1 << 4,
	/// <summary>
	/// Indicates Saturday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Saturday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Saturday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Sunday_Description))]
	Saturday = 1 << 5,
	/// <summary>
	/// Indicates Sunday as a work day.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.WorkDayTypes_Sunday_Name),
		ShortName = nameof(RESX.WorkDayTypes_Sunday_ShortName),
		Description = nameof(RESX.WorkDayTypes_Sunday_Description))]
	Sunday = 1 << 6
}
