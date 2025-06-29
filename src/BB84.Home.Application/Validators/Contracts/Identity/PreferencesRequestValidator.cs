﻿using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Domain.Enumerators.Attendance;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Identity;

/// <summary>
/// The validator for the preferences request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class PreferencesRequestValidator : AbstractValidator<PreferencesRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="PreferencesRequestValidator"/> class.
	/// </summary>
	public PreferencesRequestValidator()
	{
		When(x => x.AttendancePreferences is not null, () => RuleFor(x => x.AttendancePreferences)
		.SetValidator(new AttendancePreferencesRequestValidator()!));
	}
}

/// <summary>
/// The validator for the attendance preferences request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AttendancePreferencesRequestValidator : AbstractValidator<AttendancePreferencesRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AttendancePreferencesRequestValidator"/> class.
	/// </summary>
	public AttendancePreferencesRequestValidator()
	{
		RuleFor(x => x.WorkDays)
			.IsInEnum()
			.NotEqual(WorkDayTypes.None);

		RuleFor(x => x.WorkHours)
			.NotEmpty();

		RuleFor(x => x.VacationDays)
			.NotEmpty();
	}
}
