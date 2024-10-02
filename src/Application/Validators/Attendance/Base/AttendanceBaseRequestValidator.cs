using Application.Contracts.Requests.Attendance.Base;

using Domain.Enumerators.Attendance;

using FluentValidation;

namespace Application.Validators.Attendance.Base;

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
		AttendanceType.HOLIDAY => false,
		AttendanceType.WORKDAY => true,
		AttendanceType.ABSENCE => false,
		AttendanceType.BUISNESSTRIP => true,
		AttendanceType.SUSPENSION => false,
		AttendanceType.MOBILEWORKING => true,
		AttendanceType.PLANNEDVACATION => false,
		AttendanceType.SHORTTIMEWORK => false,
		AttendanceType.SICKNESS => false,
		AttendanceType.VACATION => false,
		AttendanceType.VACATIONBLOCK => false,
		AttendanceType.TRAINING => true,
		_ => false
	};
}
