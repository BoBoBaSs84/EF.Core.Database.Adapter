using Application.Contracts.Requests.Finance;
using Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Finance;

/// <summary>
/// The validator for the card update request.
/// </summary>
public sealed class CardUpdateRequestValidator : AbstractValidator<CardUpdateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="CardUpdateRequestValidator"/> class.
	/// </summary>
	public CardUpdateRequestValidator()
		=> Include(new CardBaseRequestValidator());
}
