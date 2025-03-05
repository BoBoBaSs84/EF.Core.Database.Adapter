using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Validators.Contracts.Attendance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Attendance;

/// <summary>
/// The validator for the attendance update request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AttendanceUpdateRequestValidator : AbstractValidator<AttendanceUpdateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AttendanceUpdateRequestValidator"/> class.
	/// </summary>
	public AttendanceUpdateRequestValidator()
	{
		Include(new AttendanceBaseRequestValidator());

		RuleFor(x => x.Id)
			.NotEqual(Guid.Empty);
	}
}
