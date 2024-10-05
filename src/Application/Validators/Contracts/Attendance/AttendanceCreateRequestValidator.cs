using Application.Contracts.Requests.Attendance;
using Application.Validators.Contracts.Attendance.Base;

using FluentValidation;

using DateStatics = Application.Common.ApplicationStatics.DateStatics;

namespace Application.Validators.Contracts.Attendance;

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
			.InclusiveBetween(DateStatics.MinDate, DateStatics.MaxDate);
	}
}
