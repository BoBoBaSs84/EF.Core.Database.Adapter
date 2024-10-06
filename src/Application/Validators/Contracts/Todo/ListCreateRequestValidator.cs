using Application.Contracts.Requests.Todo;
using Application.Validators.Contracts.Todo.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Todo;

/// <summary>
/// The validator for the todo list create request.
/// </summary>
public sealed class ListCreateRequestValidator : AbstractValidator<ListCreateRequest>
{
	/// <summary>
	/// Initializes an new instance of the <see cref="ListCreateRequestValidator"/> class.
	/// </summary>
	public ListCreateRequestValidator()
		=> Include(new ListBaseRequestValidator());
}
