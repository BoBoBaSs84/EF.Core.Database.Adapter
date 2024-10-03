﻿using Application.Contracts.Requests.Finance.Base;

using FluentValidation;

using static Application.Common.Statics;

namespace Application.Validators.Finance.Base;

/// <summary>
/// The validator for the card base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class CardBaseRequestValidator : AbstractValidator<CardBaseRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="CardBaseRequestValidator"/> class.
	/// </summary>
	public CardBaseRequestValidator()
	{
		RuleFor(x => x.Type)
			.IsInEnum();

		RuleFor(x => x.ValidUntil)
			.GreaterThanOrEqualTo(DateRanges.MinDate)
			.LessThanOrEqualTo(DateRanges.MaxDate);
	}
}