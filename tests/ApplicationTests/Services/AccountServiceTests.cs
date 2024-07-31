using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application.Finance;

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
public sealed class AccountServiceTests : ApplicationTestBase
{
	private readonly IAccountService _accountService;
	private static UserModel s_user = default!;

	public AccountServiceTests()
		=> _accountService = GetService<IAccountService>();

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
		=> s_user = DataSeedHelper.SeedUser();

	[TestMethod]
	public async Task CreateAccountSuccess()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		AccountCreateRequest request = new AccountCreateRequest().GetAccountCreateRequest();

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task CreateAccountNumberInvalid()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		AccountCreateRequest request = new() { IBAN = "UnitTest", Provider = "UnitTest" };

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.CreateAccountNumberInvalid(request.IBAN));
		});
	}

	[TestMethod]
	public async Task CreateAccountNumberConflict()
	{
		(Guid userId, _, string iban, _, _) = GetUserAccountCard();
		AccountCreateRequest request = new() { IBAN = iban, Provider = "UnitTest" };

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.CreateAccountNumberConflict(iban));
		});
	}

	[TestMethod]
	public async Task CreateAccountCardNumberInvalid()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		string pan = "UnitTest";
		AccountCreateRequest request = new AccountCreateRequest().GetAccountCreateRequest();
		CardCreateRequest cardCreateRequest = new() { PAN = pan, CardType = CardType.DEBIT };
		request.Cards = [cardCreateRequest];

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.CreateCardNumberInvalid(pan));
		});
	}

	[TestMethod]
	public async Task CreateAccountCardNumberConflict()
	{
		(Guid userId, _, _, _, string pan) = GetUserAccountCard();
		AccountCreateRequest request = new AccountCreateRequest().GetAccountCreateRequest();
		CardCreateRequest cardCreateRequest = new() { PAN = pan, CardType = CardType.DEBIT };
		request.Cards = [cardCreateRequest];

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.CreateCardNumberConflict(pan));
		});
	}

	[TestMethod]
	public async Task DeleteAccountSuccess()
	{
		(Guid userId, Guid accountId, _, _, _) = GetUserAccountCard();

		ErrorOr<Deleted> result =
			await _accountService.Delete(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task DeleteAccountNotFound()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		Guid accountId = default;

		ErrorOr<Deleted> result =
			await _accountService.Delete(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.DeleteAccountNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task GetAccountsSuccess()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();

		ErrorOr<IEnumerable<AccountResponse>> result =
			await _accountService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	public async Task GetAccountsNotFound()
	{
		Guid userId = default;

		ErrorOr<IEnumerable<AccountResponse>> result =
			await _accountService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.GetAllNotFound);
		});
	}

	[TestMethod]
	public async Task GetAccountByIdSuccess()
	{
		(Guid userId, Guid accountId, _, _, _) = GetUserAccountCard();

		ErrorOr<AccountResponse> result =
			await _accountService.Get(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetAccountByIdNotFound()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		Guid accountId = default;

		ErrorOr<AccountResponse> result =
			await _accountService.Get(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.GetByIdNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task GetAccountByNumberSuccess()
	{
		(Guid userId, _, string iban, _, _) = GetUserAccountCard();

		ErrorOr<AccountResponse> result =
			await _accountService.Get(userId, iban);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetAccountByNumberNotFound()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		string iban = string.Empty;

		ErrorOr<AccountResponse> result =
			await _accountService.Get(userId, iban);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.GetByNumberNotFound(iban));
		});
	}

	[TestMethod]
	public async Task UpdateAccountSuccess()
	{
		(Guid userId, Guid accountId, _, Guid cardId, _) = GetUserAccountCard();

		AccountUpdateRequest request = new()
		{
			Id = accountId,
			Provider = "UnitTest",
			Cards = [new CardUpdateRequest() { Id = cardId, ValidUntil = DateTime.Today }]
		};

		ErrorOr<Updated> result =
			await _accountService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod]
	public async Task UpdateAccountNotFound()
	{
		(Guid userId, _, _, _, _) = GetUserAccountCard();
		Guid accountId = default;

		AccountUpdateRequest request = new()
		{
			Id = accountId,
			Provider = "UnitTest"
		};

		ErrorOr<Updated> result =
			await _accountService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.UpdateAccountNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task UpdateAccountCardNotFound()
	{
		(Guid userId, Guid accountId, _, _, _) = GetUserAccountCard();
		Guid cardId = default;

		AccountUpdateRequest request = new()
		{
			Id = accountId,
			Provider = "UnitTest",
			Cards = [new CardUpdateRequest() { Id = cardId, ValidUntil = DateTime.Today }]
		};

		ErrorOr<Updated> result =
			await _accountService.Update(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.UpdateCardNotFound(cardId));
		});
	}

	private static (Guid UserId, Guid AccountId, string IBAN, Guid CardId, string PAN) GetUserAccountCard()
	{
		AccountModel account = s_user.AccountUsers
			.Select(x => x.Account)
			.ToList()[RandomHelper.GetInt(0, s_user.AccountUsers.Count)];
		CardModel card = account.Cards
			.ToList()[RandomHelper.GetInt(0, account.Cards.Count)];

		return (s_user.Id, account.Id, account.IBAN, card.Id, card.PAN);
	}
}