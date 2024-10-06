using Application.Contracts.Requests.Todo;
using Application.Validators.Contracts.Todo.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Todo;

/// <summary>
/// The validator for the todo list update request.
/// </summary>
public sealed class ListUpdateRequestValidator : AbstractValidator<ListUpdateRequest>
{
	/// <summary>
	/// Initializes an new instance of the <see cref="ListUpdateRequestValidator"/> class.
	/// </summary>
	public ListUpdateRequestValidator()
		=> Include(new ListBaseRequestValidator());
}
