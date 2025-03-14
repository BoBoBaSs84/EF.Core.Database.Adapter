﻿using BB84.Home.Application.Contracts.Requests.Finance.Base;
using BB84.Home.Application.Extensions;

using FluentValidation;

using DateStatics = BB84.Home.Application.Common.ApplicationStatics.DateStatics;

namespace BB84.Home.Application.Validators.Contracts.Finance.Base;

/// <summary>
/// The validator for the transaction base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class TransactionBaseRequestValidator : AbstractValidator<TransactionBaseRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="TransactionBaseRequestValidator"/> class.
	/// </summary>
	public TransactionBaseRequestValidator()
	{
		RuleFor(x => x.BookingDate)
			.NotEmpty()
			.InclusiveBetween(DateStatics.MinDate, DateStatics.MaxDate);

		When(x => x.ValueDate is not null, () =>
		{
			RuleFor(x => x.ValueDate)
				.GreaterThanOrEqualTo(x => x.BookingDate);

			RuleFor(x => x.ValueDate)
				.InclusiveBetween(DateStatics.MinDate, DateStatics.MaxDate);
		});

		RuleFor(x => x.PostingText)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.ClientBeneficiary)
			.NotEmpty()
			.MaximumLength(250);

		RuleFor(x => x.Purpose)
			.MaximumLength(4000);

		RuleFor(x => x.AccountNumber)
			.NotEmpty()
			.MaximumLength(25);

		RuleFor(x => x.BankCode)
			.NotEmpty()
			.BankIdentificationCode();

		RuleFor(x => x.AmountEur)
			.NotEmpty()
			.InclusiveBetween(-999999999.99M, 999999999.99M);

		RuleFor(x => x.CreditorId)
			.MaximumLength(25);

		RuleFor(x => x.MandateReference)
			.MaximumLength(50);

		RuleFor(x => x.CustomerReference)
			.MaximumLength(50);
	}
}
