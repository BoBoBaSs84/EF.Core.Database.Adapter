using BB84.Home.Application.Contracts.Requests.Todo.Base;
using BB84.Home.Application.Extensions;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Todo.Base;

/// <summary>
/// The validator for the todo list base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class ListBaseRequestValidator : AbstractValidator<ListBaseRequest>
{
	/// <summary>
	/// Initializes an new instance of the <see cref="ListBaseRequestValidator"/> class.
	/// </summary>
	public ListBaseRequestValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(256);

		RuleFor(x => x.Color)
			.RgbHex();
	}
}
