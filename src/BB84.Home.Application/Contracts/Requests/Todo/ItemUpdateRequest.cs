using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Todo.Base;

namespace BB84.Home.Application.Contracts.Requests.Todo;

/// <summary>
/// The request class to update an existing todo item.
/// </summary>
public sealed class ItemUpdateRequest : ItemBaseRequest
{
	/// <summary>
	/// Indicates if the todo item is done.
	/// </summary>
	[Required]
	public required bool Done { get; init; }
}
