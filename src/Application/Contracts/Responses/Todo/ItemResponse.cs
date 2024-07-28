using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Todo;

/// <summary>
/// The response fo the todo list.
/// </summary>
public sealed class ItemResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo item.
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// The note on the todo item.
	/// </summary>
	public string? Note { get; set; }

	/// <summary>
	/// The priority of the todo item.
	/// </summary>
	public PriorityLevelType Priority { get; set; }

	/// <summary>
	/// The remind date of the todo item.
	/// </summary>
	public DateTime? Reminder { get; set; }

	/// <summary>
	/// Indicates if the todo item is done.
	/// </summary>
	public bool Done { get; set; }
}
