using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Finance;

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
			.NotEmpty()
			.PermanentAccountNumber();
	}
}
