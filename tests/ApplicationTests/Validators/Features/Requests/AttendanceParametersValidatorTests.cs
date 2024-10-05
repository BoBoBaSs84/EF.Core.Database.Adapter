using Application.Features.Requests;

using Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using DRC = Application.Common.ApplicationConstants.DateRanges;
using DRS = Application.Common.ApplicationStatics.DateStatics;

namespace ApplicationTests.Validators.Features.Requests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendanceParametersValidatorTests : ApplicationTestBase
{
	private IValidator<AttendanceParameters> _validator = default!;

	[TestMethod]
	public void ParametersShouldBeValidWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		AttendanceParameters parameters = new()
		{
			Year = DRC.MinYear,
			Month = DRC.MinMonth,
			MinDate = DRS.MinDate,
			MaxDate = DRS.MaxDate,
			Type = AttendanceType.HOLIDAY
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void ParametersShouldBeInvalidWhenMinDateIsGreaterThanMaxDate()
	{
		_validator = CreateValidatorInstance();
		AttendanceParameters parameters = new()
		{
			Year = DRC.MinYear,
			Month = DRC.MinMonth,
			MinDate = DRS.MaxDate,
			MaxDate = DRS.MinDate,
			Type = AttendanceType.HOLIDAY
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.MinDate));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.MaxDate));
	}

	private static IValidator<AttendanceParameters> CreateValidatorInstance()
		=> GetService<IValidator<AttendanceParameters>>();
}