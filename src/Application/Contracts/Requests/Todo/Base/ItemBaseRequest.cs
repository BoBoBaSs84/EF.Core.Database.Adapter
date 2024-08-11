using System.ComponentModel.DataAnnotations;

using Domain.Enumerators.Todo;

namespace Application.Contracts.Requests.Todo.Base;

/// <summary>
/// The base request class for a todo item.
/// </summary>
public abstract class ItemBaseRequest
{
	/// <summary>
	/// The title of the new todo item.
	/// </summary>
	[Required, MaxLength(256)]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The note on the new todo item.
	/// </summary>
	[MaxLength(2048)]
	public string? Note { get; set; }

	/// <summary>
	/// The priority of the new todo item.
	/// </summary>
	[Required]
	public PriorityLevelType Priority { get; set; }

	/// <summary>
	/// The reminder date for the new todo item.
	/// </summary>
	public DateTime? Reminder { get; set; }
}
