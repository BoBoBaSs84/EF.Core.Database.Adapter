namespace Domain.Enumerators;

/// <summary>
/// The priority level for todo items.
/// </summary>
public enum PriorityLevelType : byte
{
	/// <summary>
	/// Indicates the the todo item has no priority.
	/// </summary>
	NONE = 0,
	/// <summary>
	/// Indicates the todo item has a low priority.
	/// </summary>
	LOW,
	/// <summary>
	/// Indicates the todo item has a medium priority.
	/// </summary>
	MEDIUM,
	/// <summary>
	/// Indicates the todo item has a high priority.
	/// </summary>	
	HIGH
}
