using BB84.Home.Application.Features.Requests;
using BB84.Home.BaseTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Features.Requests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class DocumentParametersValidatorTests : ApplicationTestBase
{
	private IValidator<DocumentParameters> _validator = default!;

	[TestMethod]
	public void ParametersShouldBeValidWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		DocumentParameters parameters = new()
		{
			Directory = @"C:\",
			Name = "UnitTest",
			ExtensionName = "txt"
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void ParametersShouldBeInValidWhenStringsAreTooLong()
	{
		_validator = CreateValidatorInstance();
		DocumentParameters parameters = new()
		{
			Directory = RandomHelper.GetString(260),
			Name = RandomHelper.GetString(260),
			ExtensionName = RandomHelper.GetString(260)
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.ExtensionName));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.Directory));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.Name));
	}

	private static IValidator<DocumentParameters> CreateValidatorInstance()
		=> GetService<IValidator<DocumentParameters>>();
}