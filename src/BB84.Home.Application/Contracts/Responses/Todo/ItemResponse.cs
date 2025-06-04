using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;
using BB84.Home.Domain.Enumerators.Todo;

namespace BB84.Home.Application.Contracts.Responses.Todo;

/// <summary>
/// The response for the todo item.
/// </summary>
public sealed class ItemResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo item.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string Title { get; init; }

	/// <summary>
	/// The note on the todo item.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Note { get; init; }

	/// <summary>
	/// The priority of the todo item.
	/// </summary>
	[Required]
	public required PriorityLevelType Priority { get; init; }

	/// <summary>
	/// The remind date of the todo item.
	/// </summary>
	[DataType(DataType.DateTime)]
	public DateTime? Reminder { get; init; }

	/// <summary>
	/// Indicates if the todo item is done.
	/// </summary>	
	[DefaultValue(false)]
	public bool Done { get; init; }
}
