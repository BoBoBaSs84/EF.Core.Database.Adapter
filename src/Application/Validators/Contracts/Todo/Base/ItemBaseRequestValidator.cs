using Application.Contracts.Requests.Todo.Base;

using FluentValidation;

using static Application.Common.ApplicationStatics;

namespace Application.Validators.Contracts.Todo.Base;

/// <summary>
/// The validator for the todo item base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class ItemBaseRequestValidator : AbstractValidator<ItemBaseRequest>
{
	/// <summary>
	/// Initializes an new instance of the <see cref="ItemBaseRequestValidator"/> class.
	/// </summary>
	public ItemBaseRequestValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(256);

		RuleFor(x => x.Note)
			.MaximumLength(2048);

		RuleFor(x => x.Priority)
			.IsInEnum();

		RuleFor(x => x.Reminder)
			.InclusiveBetween(DateStatics.MinDate, DateStatics.MaxDate);
	}
}
