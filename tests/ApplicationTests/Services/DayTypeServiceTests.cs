using Application.Contracts.Responses.Enumerator;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Domain.Errors;
using Domain.Extensions;
using FluentAssertions;
using AssertionHelper = BaseTests.Helpers.AssertionHelper;
using DayTypes = Domain.Enumerators.DayTypes;
using TestConstants = BaseTests.Constants.TestConstants;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public class DayTypeServiceTests : ApplicationBaseTests
{
	private IDayTypeService _dayTypeService = default!;

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByIdSuccessTest()
	{
		_dayTypeService = GetRequiredService<IDayTypeService>();
		DayTypes dayType = DayTypes.WEEKDAY;

		ErrorOr<DayTypeResponse> result = await _dayTypeService.GetById((int)dayType);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Name.Should().Be(dayType.GetName());
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByIdNotFoundTest()
	{
		_dayTypeService = GetRequiredService<IDayTypeService>();
		int cardTypeId = 0;

		ErrorOr<DayTypeResponse> result = await _dayTypeService.GetById(cardTypeId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DayTypeServiceErrors.GetByIdNotFound(cardTypeId));
			result.Value.Should().BeNull();
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByNameSuccessTest()
	{
		_dayTypeService = GetRequiredService<IDayTypeService>();
		DayTypes dayType = DayTypes.WEEKDAY;

		ErrorOr<DayTypeResponse> result = await _dayTypeService.GetByName(dayType.GetName());

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Id.Should().Be((int)dayType);
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByNameNotFoundTest()
	{
		_dayTypeService = GetRequiredService<IDayTypeService>();
		string cardTypeName = "UnitTest";

		ErrorOr<DayTypeResponse> result = await _dayTypeService.GetByName(cardTypeName);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DayTypeServiceErrors.GetByNameNotFound(cardTypeName));
			result.Value.Should().BeNull();
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetAllSuccessTest()
	{
		_dayTypeService = GetRequiredService<IDayTypeService>();

		ErrorOr<IEnumerable<DayTypeResponse>> result = await _dayTypeService.GetAll();

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}
}