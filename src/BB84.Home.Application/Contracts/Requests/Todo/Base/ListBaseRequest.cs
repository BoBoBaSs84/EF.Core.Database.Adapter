using System.ComponentModel.DataAnnotations;

using static BB84.Home.Application.Common.ApplicationConstants;

namespace BB84.Home.Application.Contracts.Requests.Todo.Base;

/// <summary>
/// The base request class for a todo list.
/// </summary>
public abstract class ListBaseRequest
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	[Required, MaxLength(256)]
	public required string Title { get; init; }

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	[MaxLength(7), RegularExpression(RegexPatterns.HEXRGB)]
	public string? Color { get; init; }
}
