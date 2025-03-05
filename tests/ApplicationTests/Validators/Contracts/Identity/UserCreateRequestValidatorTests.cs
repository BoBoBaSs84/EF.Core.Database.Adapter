using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Requests.Identity;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class UserCreateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<UserCreateRequest> _validator = default!;

	[DataTestMethod]
	[DataRow(""), DataRow(null)]
	public void PasswordShouldNotBeEmpty(string password)
	{
		_validator = CreateValidatorInstance();
		UserCreateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			Email = "UnitTest@UnitTest.net",
			UserName = "UserName",
			Password = password,
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Password));
	}

	[DataTestMethod]
	[DataRow(""), DataRow(null)]
	public void UserNameShouldNotBeEmpty(string userName)
	{
		_validator = CreateValidatorInstance();
		UserCreateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			Email = "UnitTest@UnitTest.net",
			UserName = userName,
			Password = "Password",
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.UserName));
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
			UserName = "SuperFancyName",
			Password = "SuperSecretPassword1234",
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<UserCreateRequest> CreateValidatorInstance()
		=> GetService<IValidator<UserCreateRequest>>();
}