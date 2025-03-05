using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Validators.Contracts.Finance.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Finance;

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
