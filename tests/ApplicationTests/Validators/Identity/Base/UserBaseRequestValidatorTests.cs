using Application.Contracts.Requests.Identity;
using Application.Contracts.Requests.Identity.Base;

using BaseTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Identity.Base;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class UserBaseRequestValidatorTests : ApplicationTestBase
{
	private IValidator<UserBaseRequest> _validator = default!;

	[TestMethod()]
	public void RequestShouldNoBeValidWhenFirstNameLastNameAndEmailAreEmpty()
	{
		_validator = CreateValidatorInstance();
		UserCreateRequest request = new()
		{
			FirstName = string.Empty,
			LastName = string.Empty,
			Email = string.Empty,
			UserName = "UserName",
			Password = "Password"
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(4);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.FirstName));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.LastName));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Email));
	}

	[TestMethod()]
	public void RequestShouldNoBeValidWhenFirstNameLastNameAndEmailAreTooLong()
	{
		_validator = CreateValidatorInstance();
		UserCreateRequest request = new()
		{
			FirstName = RandomHelper.GetString(101),
			LastName = RandomHelper.GetString(101),
			Email = RandomHelper.GetString(101),
			UserName = "UserName",
			Password = "Password"
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(3);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.FirstName));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.LastName));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Email));
	}

	[TestMethod()]
	public void RequestShouldBeValidWhenAllRulesHavePassed()
	{
		_validator = CreateValidatorInstance();
		UserCreateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			Email = "UnitTest@UnitTest.net",
			UserName = "UserName",
			Password = "Password"
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<UserBaseRequest> CreateValidatorInstance()
	=> GetService<IValidator<UserBaseRequest>>();
}