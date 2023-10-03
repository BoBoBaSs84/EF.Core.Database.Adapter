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
public sealed class AccountServiceTests : ApplicationTestBase
{
	private readonly IAccountService _accountService;

	public AccountServiceTests()
		=> _accountService = GetService<IAccountService>();

	[TestMethod]
	public async Task CreateAccountSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string iban = user.AccountUsers
			.Select(x => x.Account)
			.Select(x => x.IBAN)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string invalidPan = "UnitTest";
		AccountCreateRequest request = new AccountCreateRequest().GetAccountCreateRequest();
		CardCreateRequest cardCreateRequest = new() { PAN = invalidPan, CardType = CardType.DEBIT };
		request.Cards = new[] { cardCreateRequest };

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.CreateCardNumberInvalid(invalidPan));
		});
	}

	[TestMethod]
	public async Task CreateAccountCardNumberConflict()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string conflictPan = user.Cards
			.Select(x => x.PAN)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];
		AccountCreateRequest request = new AccountCreateRequest().GetAccountCreateRequest();
		CardCreateRequest cardCreateRequest = new() { PAN = conflictPan, CardType = CardType.DEBIT };
		request.Cards = new[] { cardCreateRequest };

		ErrorOr<Created> result =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.CreateCardNumberConflict(conflictPan));
		});
	}

	[TestMethod]
	public async Task DeleteAccountSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];

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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;

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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];

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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string iban = user.AccountUsers
			.Select(x => x.Account)
			.Select(x => x.IBAN)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];

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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
		IList<CardModel> cards = user.Cards
			.Where(x => x.AccountId.Equals(accountId) && x.UserId.Equals(userId))
			.ToList();
		Guid cardId = cards.Select(x => x.Id).ToList()[RandomHelper.GetInt(0, cards.Count)];

		AccountUpdateRequest request = new()
		{
			Id = accountId,
			Provider = "UnitTest",
			Cards = new[] { new CardUpdateRequest() { Id = cardId, ValidUntil = DateTime.Today } }
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
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
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
		Guid cardId = default;

		AccountUpdateRequest request = new()
		{
			Id = accountId,
			Provider = "UnitTest",
			Cards = new[] { new CardUpdateRequest() { Id = cardId, ValidUntil = DateTime.Today } }
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
}