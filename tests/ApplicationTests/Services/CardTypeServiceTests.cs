using Application.Contracts.Responses.Enumerator;
using Application.Errors.Services;
using Application.Interfaces.Application;

using Domain.Errors;
using Domain.Extensions;

using FluentAssertions;

using AssertionHelper = BaseTests.Helpers.AssertionHelper;
using CardTypes = Domain.Enumerators.CardTypes;
using TestConstants = BaseTests.Constants.TestConstants;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public class CardTypeServiceTests : ApplicationBaseTests
{
	private ICardTypeService _cardTypeService = default!;

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByIdSuccessTest()
	{
		_cardTypeService = GetRequiredService<ICardTypeService>();
		CardTypes cardType = CardTypes.CREDIT;

		ErrorOr<CardTypeResponse> result = await _cardTypeService.GetById((int)cardType);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Name.Should().Be(cardType.GetName());
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByIdNotFoundTest()
	{
		_cardTypeService = GetRequiredService<ICardTypeService>();
		int cardTypeId = 0;

		ErrorOr<CardTypeResponse> result = await _cardTypeService.GetById(cardTypeId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardTypeServiceErrors.GetByIdNotFound(cardTypeId));
			result.Value.Should().BeNull();
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByNameSuccessTest()
	{
		_cardTypeService = GetRequiredService<ICardTypeService>();
		CardTypes cardType = CardTypes.CREDIT;

		ErrorOr<CardTypeResponse> result = await _cardTypeService.GetByName(cardType.GetName());

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Id.Should().Be((int)cardType);
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetByNameNotFoundTest()
	{
		_cardTypeService = GetRequiredService<ICardTypeService>();
		string cardTypeName = "UnitTest";

		ErrorOr<CardTypeResponse> result = await _cardTypeService.GetByName(cardTypeName);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardTypeServiceErrors.GetByNameNotFound(cardTypeName));
			result.Value.Should().BeNull();
		});
	}

	[TestMethod, Owner(TestConstants.Bobo)]
	public async Task GetAllSuccessTest()
	{
		_cardTypeService = GetRequiredService<ICardTypeService>();

		ErrorOr<IEnumerable<CardTypeResponse>> result = await _cardTypeService.GetAll();

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}
}