using Application.Contracts.Requests.Identity;

using FluentValidation;

namespace Application.Validators.Contracts.Identity;

/// <summary>
/// The validator for the token request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class TokenRequestValidator : AbstractValidator<TokenRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AuthenticationRequestValidator"/> class.
	/// </summary>
	public TokenRequestValidator()
	{
		RuleFor(x => x.AccessToken)
			.NotEmpty();

		RuleFor(x => x.RefreshToken)
			.NotEmpty();
	}
}
