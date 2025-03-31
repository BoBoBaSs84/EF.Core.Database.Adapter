using BB84.Home.Application.Contracts.Requests.Documents.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Contracts.Documents.Base;

/// <summary>
/// The validator for the document base request.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class DocumentBaseRequestValidator : AbstractValidator<DocumentBaseRequest>
{
	/// <summary>
	/// Initializes an instance of <see cref="DocumentBaseRequestValidator"/> class.
	/// </summary>
	public DocumentBaseRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty();

		RuleFor(x => x.ExtensionName)
			.NotEmpty();

		RuleFor(x => x.Directory)
			.NotEmpty();

		RuleFor(x => x.Flags)
			.IsInEnum();

		RuleFor(x => x.CreationTime)
			.NotEmpty();

		RuleFor(x => x.Content)
			.NotEmpty();
	}
}
