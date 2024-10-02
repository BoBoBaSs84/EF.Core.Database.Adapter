using Application.Contracts.Requests.Identity.Base;

using FluentValidation;

namespace Application.Validators.Identity.Base;

/// <summary>
/// The validator for the user base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class UserBaseRequestValidator : AbstractValidator<UserBaseRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="UserBaseRequestValidator"/> class.
	/// </summary>
	public UserBaseRequestValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.LastName)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
	}
}
