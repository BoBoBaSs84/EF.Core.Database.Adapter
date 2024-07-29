using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

namespace Application.Contracts.Responses.Todo;

/// <summary>
/// The response fo the todo list.
/// </summary>
public sealed class ListResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Title { get; set; }

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Color { get; set; }

	/// <summary>
	/// The items within the todo list.
	/// </summary>
	public ItemResponse[]? Items { get; set; }
}
