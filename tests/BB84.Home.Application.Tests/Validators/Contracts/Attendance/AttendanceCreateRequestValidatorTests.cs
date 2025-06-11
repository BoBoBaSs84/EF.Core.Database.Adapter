using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using DateStatics = BB84.Home.Application.Common.ApplicationStatics.DateStatics;

namespace BB84.Home.Application.Tests.Validators.Contracts.Attendance;

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
			Date = DateStatics.MaxDate.AddDays(1),
			Type = AttendanceType.Vacation
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
			Date = DateStatics.MinDate.AddDays(-1),
			Type = AttendanceType.Absence
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