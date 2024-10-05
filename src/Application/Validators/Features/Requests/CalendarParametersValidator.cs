using Application.Features.Requests;

using FluentValidation;

using DRC = Application.Common.ApplicationConstants.DateRanges;
using DRS = Application.Common.ApplicationStatics.DateStatics;

namespace Application.Validators.Features.Requests;

/// <summary>
/// The validator for the calendar parameters.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class CalendarParametersValidator : AbstractValidator<CalendarParameters>
{
	/// <summary>
	/// Initializes an instance of <see cref="CalendarParametersValidator"/> class.
	/// </summary>
	public CalendarParametersValidator()
	{
		When(x => x.Year is not null, () => RuleFor(x => x.Year)
			.InclusiveBetween(DRC.MinYear, DRC.MaxYear));

		When(x => x.Month is not null, () => RuleFor(x => x.Month)
			.InclusiveBetween(DRC.MinMonth, DRC.MaxMonth));

		When(x => x.MinDate is not null, () => RuleFor(x => x.MinDate)
			.InclusiveBetween(DRS.MinDate, DRS.MaxDate));

		When(x => x.MaxDate is not null, () => RuleFor(x => x.MaxDate)
			.InclusiveBetween(DRS.MinDate, DRS.MaxDate));

		When(x => x.MinDate is not null && x.MaxDate is not null, () =>
		{
			RuleFor(x => x.MinDate).LessThan(x => x.MaxDate);
			RuleFor(x => x.MaxDate).GreaterThan(x => x.MinDate);
		});
	}
}
