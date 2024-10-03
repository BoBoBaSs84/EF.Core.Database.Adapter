using Application.Contracts.Requests.Identity;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AuthenticationRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AuthenticationRequest> _validator = default!;

	[DataTestMethod]
	[DataRow(""), DataRow(null)]
	public void PasswordShouldNotBeEmpty(string password)
	{
		_validator = CreateValidatorInstance();
		AuthenticationRequest request = new()
		{
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
		AuthenticationRequest request = new()
		{
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

	private static IValidator<AuthenticationRequest> CreateValidatorInstance()
		=> GetService<IValidator<AuthenticationRequest>>();
}