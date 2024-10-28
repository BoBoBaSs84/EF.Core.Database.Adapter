using Application.Contracts.Requests.Documents;
using Application.Validators.Contracts.Documents;

using ApplicationTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Documents;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class DocumentCreateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<DocumentCreateRequest> _validator = default!;

	[TestMethod]
	[TestCategory(nameof(DocumentCreateRequestValidator))]
	public void DocumentCreateRequestShouldBeValidWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		DocumentCreateRequest request = RequestHelper.GetDocumentCreateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<DocumentCreateRequest> CreateValidatorInstance()
		=> GetService<IValidator<DocumentCreateRequest>>();
}