using Application.Contracts.Requests.Attendance;

using Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using DateRanges = Application.Common.Statics.DateRanges;

namespace ApplicationTests.Validators.Attendance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendanceCreateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AttendanceCreateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldNotBeValidIfDateIsOutOfMaxRange()
	{
		_validator = CreateValidatorInstance();
		AttendanceCreateRequest request = new()
		{
			Date = DateRanges.MaxDate.AddDays(1),
			Type = AttendanceType.VACATION
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
	}

	[TestMethod]
	public void RequestShouldNotBeValidIfDateIsOutOfMinRange()
	{
		_validator = CreateValidatorInstance();
		AttendanceCreateRequest request = new()
		{
			Date = DateRanges.MinDate.AddDays(-1),
			Type = AttendanceType.ABSENCE
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
	}

	private static IValidator<AttendanceCreateRequest> CreateValidatorInstance()
		=> GetService<IValidator<AttendanceCreateRequest>>();
}