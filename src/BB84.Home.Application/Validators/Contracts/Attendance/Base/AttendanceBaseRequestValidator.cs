using BB84.Home.Application.Contracts.Requests.Attendance.Base;
using BB84.Home.Domain.Enumerators.Attendance;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Attendance.Base;

/// <summary>
/// The validator for the attendance base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AttendanceBaseRequestValidator : AbstractValidator<AttendanceBaseRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AttendanceBaseRequestValidator"/> class.
	/// </summary>
	public AttendanceBaseRequestValidator()
	{
		RuleFor(x => x.Type)
			.IsInEnum();

		When(x => IsTimeRelevant(x.Type), () =>
		{
			RuleFor(x => x.StartTime).NotNull();
			RuleFor(x => x.EndTime).NotNull();
			RuleFor(x => x.BreakTime).NotNull();
			RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
		}).Otherwise(() =>
		{
			RuleFor(x => x.StartTime).Null();
			RuleFor(x => x.EndTime).Null();
			RuleFor(x => x.BreakTime).Null();
		});
	}

	private static bool IsTimeRelevant(AttendanceType type) => type switch
	{
		AttendanceType.Holiday => false,
		AttendanceType.Workday => true,
		AttendanceType.Absence => false,
		AttendanceType.BuisnessTrip => true,
		AttendanceType.Suspension => false,
		AttendanceType.MobileWorking => true,
		AttendanceType.PlannedVacation => false,
		AttendanceType.ShortTimeWork => false,
		AttendanceType.Sickness => false,
		AttendanceType.VACATION => false,
		AttendanceType.VacationBlock => false,
		AttendanceType.Training => true,
		_ => false
	};
}
