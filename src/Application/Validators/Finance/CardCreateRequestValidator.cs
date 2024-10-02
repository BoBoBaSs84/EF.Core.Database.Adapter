using Application.Contracts.Requests.Finance;
using Application.Extensions;
using Application.Validators.Finance.Base;

using FluentValidation;

namespace Application.Validators.Finance;

/// <summary>
/// The validator for the card create request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class CardCreateRequestValidator : AbstractValidator<CardCreateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="CardCreateRequestValidator"/> class.
	/// </summary>
	public CardCreateRequestValidator()
	{
		Include(new CardBaseRequestValidator());

		RuleFor(x => x.PAN)
			.PermanentAccountNumber();
	}
}
