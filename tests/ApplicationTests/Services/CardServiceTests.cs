using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Models.Finance;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public sealed class CardServiceTests : ApplicationTestBase
{
	private readonly ICardService _cardService;
	private static UserModel s_user = default!;

	public CardServiceTests()
		=> _cardService = GetService<ICardService>();

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
		=> s_user = DataSeedHelper.SeedUser();

	[TestMethod]
	public async Task CreateCardSuccess()
	{
		(Guid userId, Guid accountId, _, _) = GetUserAccountCard();
		CardCreateRequest request = new CardCreateRequest().GetCardCreateRequest();

		ErrorOr<Created> result =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task CreateCardAccountNotFound()
	{
		(Guid userId, _, _, _) = GetUserAccountCard();
		Guid accountId = default;
		CardCreateRequest request = new CardCreateRequest().GetCardCreateRequest();

		ErrorOr<Created> result =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.CreateAccountIdNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task CreateCardNumberConflict()
	{
		(Guid userId, Guid accountId, _, string pan) = GetUserAccountCard();
		CardCreateRequest request = new()
		{
			PAN = pan,
			CardType = CardType.CREDIT,
			ValidUntil = DateTime.Today
		};

		ErrorOr<Created> result =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.CreateNumberConflict(pan));
		});
	}

	[TestMethod]
	public async Task CreateCardNumberInvalid()
	{
		(Guid userId, Guid accountId, _, _) = GetUserAccountCard();
		string pan = "UnitTest";
		CardCreateRequest request = new()
		{
			PAN = pan,
			CardType = CardType.CREDIT,
			ValidUntil = DateTime.Today
		};

		ErrorOr<Created> result =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.CreateNumberInvalid(pan));
		});
	}

	[TestMethod]
	public async Task DeleteCardSuccess()
	{
		(Guid userId, _, Guid cardId, _) = GetUserAccountCard();

		ErrorOr<Deleted> result =
			await _cardService.Delete(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task DeleteCardNotFound()
	{
		(Guid userId, _, _, _) = GetUserAccountCard();
		Guid cardId = default;

		ErrorOr<Deleted> result =
			await _cardService.Delete(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}

	[TestMethod]
	public async Task GetAllCardsSuccess()
	{
		(Guid userId, _, _, _) = GetUserAccountCard();

		ErrorOr<IEnumerable<CardResponse>> result =
			await _cardService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetAllCardsNotFound()
	{
		Guid userId = default;

		ErrorOr<IEnumerable<CardResponse>> result =
			await _cardService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.GetAllNotFound);
		});
	}

	[TestMethod]
	public async Task GetCardByIdSuccess()
	{
		(Guid userId, _, Guid cardId, _) = GetUserAccountCard();

		ErrorOr<CardResponse> result =
			await _cardService.Get(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetCardByIdNotFound()
	{
		(Guid userId, _, _, _) = GetUserAccountCard();
		Guid cardId = default;

		ErrorOr<CardResponse> result =
			await _cardService.Get(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}

	[TestMethod]
	public async Task GetCardByNumberSuccess()
	{
		(Guid userId, _, _, string pan) = GetUserAccountCard();

		ErrorOr<CardResponse> result =
			await _cardService.Get(userId, pan);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetCardByNumberNotFound()
	{
		(Guid userId, _, _, _) = GetUserAccountCard();
		string cardPan = "TestPan";

		ErrorOr<CardResponse> result =
			await _cardService.Get(userId, cardPan);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.GetByNumberNotFound(cardPan));
		});
	}

	[TestMethod]
	public async Task UpdateSuccess()
	{
		(Guid userId, _, Guid cardId, _) = GetUserAccountCard();

		CardUpdateRequest request = new() { Id = cardId, ValidUntil = DateTime.Today };

		ErrorOr<Updated> result =
			await _cardService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod]
	public async Task UpdateNotFound()
	{
		(Guid userId, _, _, _) = GetUserAccountCard();
		Guid cardId = default;

		CardUpdateRequest request = new() { Id = cardId, ValidUntil = DateTime.Today };

		ErrorOr<Updated> result =
			await _cardService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}

	private static (Guid UserId, Guid AccountId, Guid CardId, string PAN) GetUserAccountCard()
	{
		AccountModel account = s_user.AccountUsers
			.Select(x => x.Account)
			.ToList()[RandomHelper.GetInt(0, s_user.AccountUsers.Count)];
		CardModel card = account.Cards
			.ToList()[RandomHelper.GetInt(0, account.Cards.Count)];

		return (s_user.Id, account.Id, card.Id, card.PAN);
	}
}