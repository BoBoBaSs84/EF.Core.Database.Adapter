using Application.Contracts.Requests.Identity;

using Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendancePreferencesRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AttendancePreferencesRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeInvalidWhenPropertiesAreEmpty()
	{
		_validator = CreateValidatorInstance();
		AttendancePreferencesRequest request = new()
		{
			WorkDays = default,
			WorkHours = default,
			VacationDays = default
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(3);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.WorkDays));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.WorkHours));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.VacationDays));
	}

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		AttendancePreferencesRequest request = new()
		{
			WorkDays = WorkDayTypes.Monday | WorkDayTypes.Tuesday | WorkDayTypes.Wednesday | WorkDayTypes.Thursday,
			WorkHours = 30,
			VacationDays = 20
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	private static IValidator<AttendancePreferencesRequest> CreateValidatorInstance()
		=> GetService<IValidator<AttendancePreferencesRequest>>();
}