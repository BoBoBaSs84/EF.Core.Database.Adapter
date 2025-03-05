using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Validators.Features.Requests.Base;

using FluentValidation;

using DRC = BB84.Home.Application.Common.ApplicationConstants.DateRanges;
using DRS = BB84.Home.Application.Common.ApplicationStatics.DateStatics;

namespace BB84.Home.Application.Validators.Features.Requests;

/// <summary>
/// The validator for the attendance parameters.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AttendanceParametersValidator : AbstractValidator<AttendanceParameters>
{
	/// <summary>
	/// Initializes an instance of <see cref="AttendanceParametersValidator"/> class.
	/// </summary>
	public AttendanceParametersValidator()
	{
		Include(new RequestParametersValidator());

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

		When(x => x.Type is not null, () => RuleFor(x => x.Type)
			.IsInEnum());
	}
}
