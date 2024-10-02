using Application.Contracts.Requests.Identity;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class PreferencesRequestValidatorTests : ApplicationTestBase
{
	private IValidator<PreferencesRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeValidWhenAttendancePreferencesAreNull()
	{
		_validator = CreateValidatorInstance();
		PreferencesRequest request = new();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void RequestShouldBeInvalidWhenAttendancePreferencesAreNotCorrect()
	{
		_validator = CreateValidatorInstance();
		PreferencesRequest request = new()
		{
			AttendancePreferences = new()
			{
				VacationDays = default,
				WorkDays = default,
				WorkHours = default
			}
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
	}

	private static IValidator<PreferencesRequest> CreateValidatorInstance()
		=> GetService<IValidator<PreferencesRequest>>();
}