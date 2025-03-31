using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Finance;

/// <summary>
/// The validator for the account update request.
/// </summary>
public sealed class AccountUpdateRequestValidator : AbstractValidator<AccountUpdateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="AccountUpdateRequestValidator"/> class.
	/// </summary>
	public AccountUpdateRequestValidator()
		=> Include(new AccountBaseRequestValidator());
}
