using Application.Contracts.Requests.Attendance;

using Domain.Enumerators.Attendance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Attendance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AttendanceUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AttendanceUpdateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldNotBeValidIfIdIsEmpty()
	{
		_validator = CreateValidatorInstance();
		AttendanceUpdateRequest request = new()
		{
			Id = Guid.Empty,
			Type = AttendanceType.SHORTTIMEWORK
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().HaveCount(1);
	}

	private static IValidator<AttendanceUpdateRequest> CreateValidatorInstance()
	=> GetService<IValidator<AttendanceUpdateRequest>>();
}