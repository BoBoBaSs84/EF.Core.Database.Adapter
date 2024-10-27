using Application.Contracts.Requests.Documents;
using Application.Validators.Contracts.Documents.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Documents;

/// <summary>
/// The validator for the document create request.
/// </summary>
public sealed class DocumentCreateRequestValidator : AbstractValidator<DocumentCreateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="DocumentCreateRequestValidator"/> class.
	/// </summary>
	public DocumentCreateRequestValidator()
		=> Include(new DocumentBaseRequestValidator());
}
