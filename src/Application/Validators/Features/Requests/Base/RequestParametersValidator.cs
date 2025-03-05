using BB84.Home.Application.Features.Requests.Base;

using FluentValidation;

namespace BB84.Home.Application.Validators.Features.Requests.Base;

/// <summary>
/// The validator for the request parameters.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent validation.")]
public sealed class RequestParametersValidator : AbstractValidator<RequestParameters>
{
	/// <summary>
	/// Initializes an instance of <see cref="RequestParametersValidator"/> class.
	/// </summary>
	public RequestParametersValidator()
	{
		RuleFor(x => x.PageNumber)
			.GreaterThan(0);

		RuleFor(x => x.PageSize)
			.NotEmpty()
			.InclusiveBetween(10, 100);
	}
}
