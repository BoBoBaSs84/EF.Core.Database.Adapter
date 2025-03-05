using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Validators.Features.Requests.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Features.Requests;

/// <summary>
/// The validator for the document parameters.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class DocumentParametersValidator : AbstractValidator<DocumentParameters>
{
	/// <summary>
	/// Initializes an instance of <see cref="DocumentParametersValidator"/> class.
	/// </summary>
	public DocumentParametersValidator()
	{
		Include(new RequestParametersValidator());

		When(x => x.Directory is not null, () => RuleFor(x => x.Directory)
			.MaximumLength(256));

		When(x => x.ExtensionName is not null, () => RuleFor(x => x.ExtensionName)
			.MaximumLength(256));

		When(x => x.Name is not null, () => RuleFor(x => x.Name)
			.MaximumLength(256));
	}
}
