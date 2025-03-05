using BaseTests.Constants;
using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Tests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class TransactionUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<TransactionUpdateRequest>? _validator;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		TransactionUpdateRequest request = RequestHelper.GetTransactionUpdateRequest();

		ValidationResult result = _validator.Validate(request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeEmpty();
		});
	}

	[TestMethod]
	public void RequestShouldNotBeValidWhenPropertiesNotAreCorrect()
	{
		_validator = CreateValidatorInstance();
		TransactionUpdateRequest request = new()
		{
			BookingDate = DateTime.MaxValue,
			ValueDate = DateTime.MinValue,
			PostingText = RandomHelper.GetString(101),
			ClientBeneficiary = RandomHelper.GetString(251),
			Purpose = RandomHelper.GetString(4001),
			AccountNumber = RandomHelper.GetString(26),
			BankCode = RandomHelper.GetString(20, TestConstants.WildCardChars),
			AmountEur = 0,
			CreditorId = RandomHelper.GetString(26),
			MandateReference = RandomHelper.GetString(51),
			CustomerReference = RandomHelper.GetString(51)
		};

		ValidationResult result = _validator.Validate(request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.Errors.Should().HaveCount(12);
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.BookingDate));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.ValueDate));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.PostingText));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.ClientBeneficiary));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Purpose));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.AccountNumber));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.BankCode));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.AmountEur));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.CreditorId));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.MandateReference));
			result.Errors.Should().Contain(x => x.PropertyName == nameof(request.CustomerReference));
		});
	}

	private static IValidator<TransactionUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<TransactionUpdateRequest>>();
}