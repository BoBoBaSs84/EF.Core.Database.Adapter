using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Validators.Contracts.Identity.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Identity;

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
