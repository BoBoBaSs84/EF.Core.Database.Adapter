using Application.Contracts.Requests.Todo;
using Application.Validators.Contracts.Todo.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Todo;

/// <summary>
/// The validator for the todo item update request.
/// </summary>
public sealed class ItemUpdateRequestValidator : AbstractValidator<ItemUpdateRequest>
{
	/// <summary>
	/// Initializes an new instance of the <see cref="ItemUpdateRequestValidator"/> class.
	/// </summary>
	public ItemUpdateRequestValidator()
		=> Include(new ItemBaseRequestValidator());
}
