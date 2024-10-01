using Application.Contracts.Requests.Identity;

using FluentValidation;

namespace Application.Validators.Identity;

/// <summary>
/// The validator for the authentication request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AuthenticationRequestValidator"/> class.
	/// </summary>
	public AuthenticationRequestValidator()
	{
		RuleFor(x => x.UserName).NotEmpty();
		RuleFor(x => x.Password).NotEmpty();
	}
}
