using BB84.Home.Application.Contracts.Requests.Identity;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Identity;

[TestClass()]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class TokenRequestValidatorTests : ApplicationTestBase
{
	private IValidator<TokenRequest> _validator = default!;

	[DataTestMethod]
	[DataRow(""), DataRow(null)]
	public void AccessTokenShouldNotBeEmpty(string accessToken)
	{
		_validator = CreateValidatorInstance();
		TokenRequest request = new()
		{
			AccessToken = accessToken,
			RefreshToken = nameof(TokenRequest.RefreshToken)
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.AccessToken));
	}

	[DataTestMethod]
	[DataRow(""), DataRow(null)]
	public void RefreshTokenShouldNotBeEmpty(string refreshToken)
	{
		_validator = CreateValidatorInstance();
		TokenRequest request = new()
		{
			AccessToken = nameof(TokenRequest.AccessToken),
			RefreshToken = refreshToken
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.RefreshToken));
	}

	private static IValidator<TokenRequest> CreateValidatorInstance()
		=> GetService<IValidator<TokenRequest>>();
}