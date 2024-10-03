using Application.Contracts.Requests.Attendance;
using Application.Contracts.Requests.Attendance.Base;

using Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using static Application.Common.Statics;

namespace ApplicationTests.Validators.Attendance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendanceBaseRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AttendanceBaseRequest> _validator = default!;

	[DataTestMethod]
	[DataRow(AttendanceType.WORKDAY)]
	[DataRow(AttendanceType.BUISNESSTRIP)]
	[DataRow(AttendanceType.MOBILEWORKING)]
	[DataRow(AttendanceType.TRAINING)]
	public void RequestShouldOnlyHaveTimesWhenTimeRelevantTypes(AttendanceType type)
	{
		_validator = CreateValidatorInstance();
		AttendanceUpdateRequest request = new()
		{
			Id = Guid.NewGuid(),
			Type = type,
			StartTime = new(6, 0, 0),
			EndTime = new(16, 0, 0),
			BreakTime = new(1, 0, 0)
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[DataTestMethod]
	[DataRow(AttendanceType.HOLIDAY)]
	[DataRow(AttendanceType.ABSENCE)]
	[DataRow(AttendanceType.SUSPENSION)]
	[DataRow(AttendanceType.PLANNEDVACATION)]
	[DataRow(AttendanceType.SHORTTIMEWORK)]
	[DataRow(AttendanceType.SICKNESS)]
	[DataRow(AttendanceType.VACATION)]
	[DataRow(AttendanceType.VACATIONBLOCK)]
	public void RequestShouldNotHaveTimesWhenNotTimeRelevantTypes(AttendanceType type)
	{
		_validator = CreateValidatorInstance();
		AttendanceCreateRequest request = new()
		{
			Date = new(2000, 1, 1),
			Type = type,
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void RequestShouldNotBeValidIfTimeIsMissing()
	{
		_validator = CreateValidatorInstance();
		AttendanceCreateRequest request = new()
		{
			Date = new(2000, 1, 1),
			Type = AttendanceType.WORKDAY
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(3);
	}

	[TestMethod]
	public void AttendanceCreateRequestShouldNotBeValidIfTimeIsNotRelevant()
	{
		_validator = CreateValidatorInstance();
		AttendanceUpdateRequest request = new()
		{
			Id = Guid.NewGuid(),
			Type = AttendanceType.HOLIDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(16, 0, 0),
			BreakTime = new(1, 0, 0)
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(3);
	}

	[TestMethod]
	public void RequestShouldNotBeValidIfStartDateIsGreaterThanEndDate()
	{
		_validator = CreateValidatorInstance();
		AttendanceCreateRequest request = new()
		{
			Date = DateRanges.MaxDate,
			Type = AttendanceType.MOBILEWORKING,
			StartTime = new(16, 0, 0),
			EndTime = new(6, 0, 0),
			BreakTime = new(1, 0, 0)
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
	}

	private static IValidator<AttendanceBaseRequest> CreateValidatorInstance()
		=> GetService<IValidator<AttendanceBaseRequest>>();
}