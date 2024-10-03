using Application.Contracts.Requests.Finance;
using Application.Extensions;
using Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Finance;

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
			.InternationalBankAccountNumber();
	}
}
