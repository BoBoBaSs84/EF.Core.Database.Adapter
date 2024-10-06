using Application.Contracts.Requests.Todo;
using Application.Validators.Contracts.Todo.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Todo;

/// <summary>
/// The validator for the todo item create request.
/// </summary>
public sealed class ItemCreateRequestValidator : AbstractValidator<ItemCreateRequest>
{
	/// <summary>
	/// Initializes an new instance of the <see cref="ItemCreateRequestValidator"/> class.
	/// </summary>
	public ItemCreateRequestValidator()
		=> Include(new ItemBaseRequestValidator());
}
