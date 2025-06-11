using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Contracts.Requests.Attendance.Base;
using BB84.Home.Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using static BB84.Home.Application.Common.ApplicationStatics;

namespace BB84.Home.Application.Tests.Validators.Contracts.Attendance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendanceBaseRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AttendanceBaseRequest> _validator = default!;

	[DataTestMethod]
	[DataRow(AttendanceType.Workday)]
	[DataRow(AttendanceType.BuisnessTrip)]
	[DataRow(AttendanceType.MobileWorking)]
	[DataRow(AttendanceType.Training)]
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
	[DataRow(AttendanceType.Holiday)]
	[DataRow(AttendanceType.Absence)]
	[DataRow(AttendanceType.Suspension)]
	[DataRow(AttendanceType.PlannedVacation)]
	[DataRow(AttendanceType.ShortTimeWork)]
	[DataRow(AttendanceType.Sickness)]
	[DataRow(AttendanceType.Vacation)]
	[DataRow(AttendanceType.VacationBlock)]
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
			Type = AttendanceType.Workday
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
			Type = AttendanceType.Holiday,
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
			Date = DateStatics.MaxDate,
			Type = AttendanceType.MobileWorking,
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