using Application.Contracts.Requests.Finance;

using ApplicationTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class TransactionUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<TransactionUpdateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeValiedWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		TransactionUpdateRequest request = RequestHelper.GetTransactionUpdateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<TransactionUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<TransactionUpdateRequest>>();
}