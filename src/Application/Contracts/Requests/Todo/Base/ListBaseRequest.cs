using System.ComponentModel.DataAnnotations;

using static Domain.Constants.DomainConstants;

namespace Application.Contracts.Requests.Todo.Base;

/// <summary>
/// The base request class for a todo list.
/// </summary>
public abstract class ListBaseRequest
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	[Required, MaxLength(256), DataType(DataType.Text)]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	[MaxLength(7), RegularExpression(RegexPatterns.HEXRGB)]
	public string? Color { get; set; }
}
