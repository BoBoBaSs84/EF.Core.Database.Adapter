using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public sealed class CardServiceTests : ApplicationTestBase
{
	private readonly ICardService _cardService;

	public CardServiceTests()
		=> _cardService = GetService<ICardService>();

	[TestMethod]
	public async Task CreateCardSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
		CardCreateRequest request = new CardCreateRequest().GetCardCreateRequest();

		ErrorOr<Created> response =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task CreateCardAccountNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = default;
		CardCreateRequest request = new CardCreateRequest().GetCardCreateRequest();

		ErrorOr<Created> response =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.CreateAccountIdNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task CreateCardNumberConflict()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
		string conflictPan = user.Cards
			.Select(x => x.PAN)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];
		CardCreateRequest request =
			new() { PAN = conflictPan, CardType = CardType.CREDIT, ValidUntil = DateTime.Today };

		ErrorOr<Created> response =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.CreateNumberConflict(conflictPan));
		});
	}

	[TestMethod]
	public async Task CreateCardNumberInvalid()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
		string invalidPan = "UnitTest";
		CardCreateRequest request =
			new() { PAN = invalidPan, CardType = CardType.CREDIT, ValidUntil = DateTime.Today };

		ErrorOr<Created> response =
			await _cardService.Create(userId, accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.CreateNumberInvalid(invalidPan));
		});
	}

	[TestMethod]
	public async Task DeleteCardSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid cardId = user.Cards
			.Select(x => x.Id)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];

		ErrorOr<Deleted> response =
			await _cardService.Delete(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task DeleteCardNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid cardId = default;

		ErrorOr<Deleted> response =
			await _cardService.Delete(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}

	[TestMethod]
	public async Task GetAllCardsSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;

		ErrorOr<IEnumerable<CardResponse>> response =
			await _cardService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetAllCardsNotFound()
	{
		Guid userId = default;

		ErrorOr<IEnumerable<CardResponse>> response =
			await _cardService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.GetAllNotFound);
		});
	}

	[TestMethod]
	public async Task GetCardByIdSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid cardId = user.Cards
			.Select(x => x.Id)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];

		ErrorOr<CardResponse> response =
			await _cardService.Get(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetCardByIdNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid cardId = default;

		ErrorOr<CardResponse> response =
			await _cardService.Get(userId, cardId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}

	[TestMethod]
	public async Task GetCardByNumberSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string cardPan = user.Cards
			.Select(x => x.PAN)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];

		ErrorOr<CardResponse> response =
			await _cardService.Get(userId, cardPan);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetCardByNumberNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string cardPan = "TestPan";

		ErrorOr<CardResponse> response =
			await _cardService.Get(userId, cardPan);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.GetByNumberNotFound(cardPan));
		});
	}

	[TestMethod]
	public async Task UpdateSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid cardId = user.Cards
			.Select(x => x.Id)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];

		CardUpdateRequest request = new() { Id = cardId, ValidUntil = DateTime.Today };

		ErrorOr<Updated> response =
			await _cardService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod]
	public async Task UpdateNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid cardId = default;

		CardUpdateRequest request = new() { Id = cardId, ValidUntil = DateTime.Today };

		ErrorOr<Updated> response =
			await _cardService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}
}