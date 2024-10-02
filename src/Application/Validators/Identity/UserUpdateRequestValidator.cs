using Application.Contracts.Requests.Identity;
using Application.Extensions;

using FluentValidation;

namespace Application.Validators.Identity;

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
		RuleFor(x => x.MiddleName).MaximumLength(100);
		RuleFor(x => x.PhoneNumber).PhoneNumber();
	}
}
