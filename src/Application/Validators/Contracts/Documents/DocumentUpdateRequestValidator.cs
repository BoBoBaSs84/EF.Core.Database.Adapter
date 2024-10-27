﻿using Application.Contracts.Requests.Documents;
using Application.Validators.Contracts.Documents.Base;

using FluentValidation;

namespace Application.Validators.Contracts.Documents;

/// <summary>
/// The validator for the document update request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class DocumentUpdateRequestValidator : AbstractValidator<DocumentUpdateRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="DocumentUpdateRequestValidator"/> class.
	/// </summary>
	public DocumentUpdateRequestValidator()
	{
		Include(new DocumentBaseRequestValidator());

		RuleFor(x => x.Id)
			.NotEmpty();
	}
}