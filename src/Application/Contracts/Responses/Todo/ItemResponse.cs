using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

using BB84.Home.Domain.Enumerators.Todo;

namespace Application.Contracts.Responses.Todo;

/// <summary>
/// The response for the todo item.
/// </summary>
public sealed class ItemResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo item.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The note on the todo item.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Note { get; set; }

	/// <summary>
	/// The priority of the todo item.
	/// </summary>
	[Required]
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
