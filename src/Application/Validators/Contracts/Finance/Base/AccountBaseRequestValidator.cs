using BB84.Home.Application.Contracts.Requests.Finance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Finance.Base;

/// <summary>
/// The validator for the account base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AccountBaseRequestValidator : AbstractValidator<AccountBaseRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AccountBaseRequestValidator"/> class.
	/// </summary>
	public AccountBaseRequestValidator()
	{
		RuleFor(x => x.Type)
			.IsInEnum();

		RuleFor(x => x.Provider)
			.NotEmpty()
			.MaximumLength(500);
	}
}
