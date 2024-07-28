using System.ComponentModel.DataAnnotations;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Application.Contracts.Requests.Todo;

/// <summary>
/// The request class for creating a new todo list.
/// </summary>
public sealed class ListCreateRequest
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	[MaxLength(256)]
	public string Title { get; set; } = default!;

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	[MaxLength(7), RegularExpression(RegexPatterns.HEXRGB)]
	public string? Color { get; set; }
}
