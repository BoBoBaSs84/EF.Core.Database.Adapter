using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendancePreferencesRequestValidatorTests : ApplicationTestBase
{
	private readonly IValidator<AttendancePreferencesRequest> _validator;

	public AttendancePreferencesRequestValidatorTests()
		=> _validator = GetService<IValidator<AttendancePreferencesRequest>>();

	[TestMethod]
	public void RequestShouldBeInvalidWhenPropertiesAreEmpty()
	{
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
		AttendancePreferencesRequest request = new()
		{
			WorkDays = WorkDayTypes.Monday | WorkDayTypes.Tuesday | WorkDayTypes.Wednesday | WorkDayTypes.Thursday,
			WorkHours = 28,
			VacationDays = 20
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}
}
