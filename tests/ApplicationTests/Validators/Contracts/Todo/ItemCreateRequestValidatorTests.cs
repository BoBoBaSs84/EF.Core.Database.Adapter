using Application.Contracts.Requests.Todo;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Todo;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class ItemCreateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<ItemCreateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void RequestShouldBeInvalidWhenPropertiesAreNotCorrect()
	{
		_validator = CreateValidatorInstance();
		ItemCreateRequest request = new()
		{
			Title = RandomHelper.GetString(300),
			Priority = (Domain.Enumerators.Todo.PriorityLevelType)12,
			Note = RandomHelper.GetString(4000),
			Reminder = DateTime.MaxValue
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(4);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Title));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Priority));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Note));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Reminder));
	}

	private static IValidator<ItemCreateRequest> CreateValidatorInstance()
		=> GetService<IValidator<ItemCreateRequest>>();
}