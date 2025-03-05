using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Finance;

/// <summary>
/// The validator for the account create request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class AccountCreateRequestValidator : AbstractValidator<AccountCreateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AccountCreateRequestValidator"/> class.
	/// </summary>
	public AccountCreateRequestValidator()
	{
		Include(new AccountBaseRequestValidator());

		RuleFor(x => x.IBAN)
			.NotEmpty()
			.InternationalBankAccountNumber();
	}
}
