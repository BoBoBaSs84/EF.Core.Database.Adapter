using Application.Contracts.Requests.Identity;
using Application.Validators.Identity.Base;

using FluentValidation;

namespace Application.Validators.Identity;

/// <summary>
/// The validator for the user base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="UserCreateRequestValidator"/> class.
	/// </summary>
	public UserCreateRequestValidator()
	{
		Include(new UserBaseRequestValidator());

		RuleFor(x => x.UserName)
			.NotEmpty();

		RuleFor(x => x.Password)
			.NotEmpty();
	}
}
