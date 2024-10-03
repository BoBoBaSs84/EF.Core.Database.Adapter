using Application.Contracts.Requests.Attendance;
using Application.Validators.Attendance.Base;

using FluentValidation;

using DateRanges = Application.Common.Statics.DateRanges;

namespace Application.Validators.Attendance;

/// <summary>
/// The validator for the attendance create request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AttendanceCreateRequestValidator : AbstractValidator<AttendanceCreateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AttendanceCreateRequestValidator"/> class.
	/// </summary>
	public AttendanceCreateRequestValidator()
	{
		Include(new AttendanceBaseRequestValidator());

		RuleFor(x => x.Date)
			.InclusiveBetween(DateRanges.MinDate, DateRanges.MaxDate);
	}
}
