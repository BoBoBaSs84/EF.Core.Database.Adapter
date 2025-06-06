﻿using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Validators.Contracts.Identity.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Identity;

/// <summary>
/// The validator for the user base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="UserUpdateRequestValidator"/> class.
	/// </summary>
	public UserUpdateRequestValidator()
	{
		Include(new UserBaseRequestValidator());

		RuleFor(x => x.MiddleName)
			.MaximumLength(100);

		RuleFor(x => x.PhoneNumber)
			.PhoneNumber();
	}
}
