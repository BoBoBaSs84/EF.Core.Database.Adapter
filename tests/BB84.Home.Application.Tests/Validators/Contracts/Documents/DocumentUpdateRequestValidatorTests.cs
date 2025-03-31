using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Application.Validators.Contracts.Documents;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Documents;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class DocumentUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<DocumentUpdateRequest> _validator = default!;

	[TestMethod]
	[TestCategory(nameof(DocumentUpdateRequestValidator))]
	public void DocumentUpdateRequestShouldBeValidWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<DocumentUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<DocumentUpdateRequest>>();
}