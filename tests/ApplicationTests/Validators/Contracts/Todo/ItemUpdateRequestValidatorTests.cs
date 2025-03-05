using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Domain.Enumerators.Todo;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Todo;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class ItemUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<ItemUpdateRequest> _validator = default!;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void RequestShouldBeInvalidWhenPropertiesAreNotCorrect()
	{
		_validator = CreateValidatorInstance();
		ItemUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(300),
			Priority = (PriorityLevelType)12,
			Note = RandomHelper.GetString(4000),
			Reminder = DateTime.MaxValue,
			Done = true
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

	private static IValidator<ItemUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<ItemUpdateRequest>>();
}