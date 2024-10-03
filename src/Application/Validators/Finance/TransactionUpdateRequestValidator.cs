using Application.Contracts.Requests.Finance;
using Application.Validators.Finance.Base;

using FluentValidation;

namespace Application.Validators.Finance;

/// <summary>
/// The validator for the transaction update request.
/// </summary>
public sealed class TransactionUpdateRequestValidator : AbstractValidator<TransactionUpdateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="TransactionUpdateRequestValidator"/> class.
	/// </summary>
	public TransactionUpdateRequestValidator()
		=> Include(new TransactionBaseRequestValidator());
}
