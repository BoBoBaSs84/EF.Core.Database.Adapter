using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Finance;

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
