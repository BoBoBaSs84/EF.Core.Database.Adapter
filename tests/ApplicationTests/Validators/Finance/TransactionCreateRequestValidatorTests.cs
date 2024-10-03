using Application.Contracts.Requests.Finance;

using ApplicationTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class TransactionCreateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<TransactionCreateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeValiedWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		TransactionCreateRequest request = RequestHelper.GetTransactionCreateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<TransactionCreateRequest> CreateValidatorInstance()
		=> GetService<IValidator<TransactionCreateRequest>>();
}