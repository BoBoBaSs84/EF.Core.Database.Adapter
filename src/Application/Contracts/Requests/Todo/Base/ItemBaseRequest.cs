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
	public required string Title { get; init; }

	/// <summary>
	/// The note on the new todo item.
	/// </summary>
	[MaxLength(2048)]
	public string? Note { get; init; }

	/// <summary>
	/// The priority of the new todo item.
	/// </summary>
	[Required]
	public required PriorityLevelType Priority { get; init; }

	/// <summary>
	/// The reminder date for the new todo item.
	/// </summary>
	public DateTime? Reminder { get; init; }
}
