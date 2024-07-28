using System.ComponentModel.DataAnnotations;

using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators;

/// <summary>
/// The priority level for todo items.
/// </summary>
public enum PriorityLevelType : byte
{
	/// <summary>
	/// Indicates the the todo item has no priority.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.PriorityLevelType_None_Name),
		ShortName = nameof(RESX.PriorityLevelType_None_ShortName),
		Description = nameof(RESX.PriorityLevelType_None_Description))]
	NONE = 0,
	/// <summary>
	/// Indicates the todo item has a low priority.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.PriorityLevelType_Low_Name),
		ShortName = nameof(RESX.PriorityLevelType_Low_ShortName),
		Description = nameof(RESX.PriorityLevelType_Low_Description))]
	LOW,
	/// <summary>
	/// Indicates the todo item has a medium priority.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.PriorityLevelType_Medium_Name),
		ShortName = nameof(RESX.PriorityLevelType_Medium_ShortName),
		Description = nameof(RESX.PriorityLevelType_Medium_Description))]
	MEDIUM,
	/// <summary>
	/// Indicates the todo item has a high priority.
	/// </summary>	
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.PriorityLevelType_High_Name),
		ShortName = nameof(RESX.PriorityLevelType_High_ShortName),
		Description = nameof(RESX.PriorityLevelType_High_Description))]
	HIGH
}
