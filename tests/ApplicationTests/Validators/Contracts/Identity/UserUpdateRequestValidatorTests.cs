using Application.Contracts.Requests.Identity;

using BaseTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class UserUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<UserUpdateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldHaveValidPhoneNumber()
	{
		_validator = CreateValidatorInstance();

		UserUpdateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			Email = "UnitTest@UnitTest.net",
			PhoneNumber = RandomHelper.GetString(50)
		};

		ValidationResult result = _validator.Validate(request);

		Assert.IsNotNull(result);
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.PhoneNumber));
	}

	[TestMethod]
	public void RequestShouldNotHaveMiddleNameThatIsTooLong()
	{
		_validator = CreateValidatorInstance();

		UserUpdateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			MiddleName = RandomHelper.GetString(101),
			LastName = RandomHelper.GetString(50),
			Email = "UnitTest@UnitTest.net",
			PhoneNumber = "+01017182222222"
		};

		ValidationResult result = _validator.Validate(request);

		Assert.IsNotNull(result);
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.MiddleName));
	}

	[TestMethod()]
	public void RequestShouldBeValidWhenAllRulesHavePassed()
	{
		_validator = CreateValidatorInstance();
		UserUpdateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			MiddleName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			DateOfBirth = DateTime.Today,
			Picture = [12, 23, 34, 45, 56, 67, 78, 89],
			Email = "UnitTest@UnitTest.net",
			PhoneNumber = "+01017182222222"
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<UserUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<UserUpdateRequest>>();
}