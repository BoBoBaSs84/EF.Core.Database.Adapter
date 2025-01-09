using Application.Contracts.Requests.Finance;

using Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Finance;

/// <summary>
/// The validator for the transaction create request.
/// </summary>
public sealed class TransactionCreateRequestValidator : AbstractValidator<TransactionCreateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="TransactionCreateRequestValidator"/> class.
	/// </summary>
	public TransactionCreateRequestValidator()
		=> Include(new TransactionBaseRequestValidator());
}
