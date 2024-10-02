using Application.Contracts.Requests.Finance;
using Application.Validators.Finance.Base;

using FluentValidation;

namespace Application.Validators.Finance;

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
