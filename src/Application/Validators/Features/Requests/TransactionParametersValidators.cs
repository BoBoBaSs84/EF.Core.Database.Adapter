using Application.Features.Requests;

using FluentValidation;

using DRS = Application.Common.Statics.DateRanges;

namespace Application.Validators.Features.Requests;

/// <summary>
/// The validator for the transaction parameters.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class TransactionParametersValidators : AbstractValidator<TransactionParameters>
{
	/// <summary>
	/// Initializes an instance of <see cref="TransactionParametersValidators"/> class.
	/// </summary>
	public TransactionParametersValidators()
	{
		When(x => x.BookingDate is not null, () => RuleFor(x => x.BookingDate)
			.InclusiveBetween(DRS.MinDate, DRS.MaxDate));

		When(x => x.ValueDate is not null, () => RuleFor(x => x.ValueDate)
			.InclusiveBetween(DRS.MinDate, DRS.MaxDate));

		When(x => x.Beneficiary is not null, () => RuleFor(x => x.Beneficiary)
			.MaximumLength(250));

		When(x => x.MinValue is not null, () => RuleFor(x => x.MinValue)
			.InclusiveBetween(0.01M, 999999999.99M));

		When(x => x.MaxValue is not null, () => RuleFor(x => x.MinValue)
			.InclusiveBetween(0.01M, 999999999.99M));

		When(x => x.MinValue is not null && x.MaxValue is not null, () =>
		{
			RuleFor(x => x.MinValue).LessThan(x => x.MaxValue);
			RuleFor(x => x.MaxValue).GreaterThan(x => x.MinValue);
		});
	}
}
