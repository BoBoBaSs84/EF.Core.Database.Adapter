using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;

namespace BB84.Home.Application.Contracts.Responses.Todo;

/// <summary>
/// The response for the todo list.
/// </summary>
public sealed class ListResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public string Title { get; set; } = string.Empty;

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
