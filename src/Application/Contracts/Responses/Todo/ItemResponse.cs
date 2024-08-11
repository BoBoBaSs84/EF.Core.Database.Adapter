using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

using Domain.Enumerators.Todo;

namespace Application.Contracts.Responses.Todo;

/// <summary>
/// The response fo the todo list.
/// </summary>
public sealed class ItemResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo item.
	/// </summary>
	[DataType(DataType.Text)]
	public string Title { get; set; } = default!;

	/// <summary>
	/// The note on the todo item.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Note { get; set; }

	/// <summary>
	/// The priority of the todo item.
	/// </summary>
	public PriorityLevelType Priority { get; set; }

	/// <summary>
	/// The remind date of the todo item.
	/// </summary>
	[DataType(DataType.DateTime)]
	public DateTime? Reminder { get; set; }

	/// <summary>
	/// Indicates if the todo item is done.
	/// </summary>	
	public bool Done { get; set; }
}
