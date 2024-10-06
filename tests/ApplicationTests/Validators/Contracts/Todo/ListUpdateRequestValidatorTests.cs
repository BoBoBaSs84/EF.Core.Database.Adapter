using Application.Contracts.Requests.Todo;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Todo;

[TestClass]

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class ListupdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<ListUpdateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void RequestShouldBeInvalidWhenPropertiesAreNotCorrect()
	{
		_validator = CreateValidatorInstance();
		ListUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(300),
			Color = RandomHelper.GetString(7)
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(2);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Title));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Color));
	}

	private static IValidator<ListUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<ListUpdateRequest>>();
}