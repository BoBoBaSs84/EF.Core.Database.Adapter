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

		ErrorOr<Created> response =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task CreateAccountNumberInvalid()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		AccountCreateRequest request = new() { IBAN = "UnitTest", Provider = "UnitTest" };

		ErrorOr<Created> response =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.CreateAccountNumberInvalid(request.IBAN));
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

		ErrorOr<Created> response =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.CreateAccountNumberConflict(iban));
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

		ErrorOr<Created> response =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.CreateCardNumberInvalid(invalidPan));
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

		ErrorOr<Created> response =
			await _accountService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.CreateCardNumberConflict(conflictPan));
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

		ErrorOr<Deleted> response =
			await _accountService.Delete(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task DeleteAccountNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = default;

		ErrorOr<Deleted> response =
			await _accountService.Delete(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.DeleteAccountNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task GetAccountsSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;

		ErrorOr<IEnumerable<AccountResponse>> response =
			await _accountService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	public async Task GetAccountsNotFound()
	{
		Guid userId = default;

		ErrorOr<IEnumerable<AccountResponse>> response =
			await _accountService.Get(userId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.GetAllNotFound);
		});
	}

	[TestMethod]
	public async Task GetAccountsByAccountIdSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];

		ErrorOr<AccountResponse> response =
			await _accountService.Get(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetAccountsByAccountIdNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		Guid accountId = default;

		ErrorOr<AccountResponse> response =
			await _accountService.Get(userId, accountId);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.GetByIdNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task GetAccountsByIbanSuccess()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string iban = user.AccountUsers
			.Select(x => x.Account)
			.Select(x => x.IBAN)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];

		ErrorOr<AccountResponse> response =
			await _accountService.Get(userId, iban);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeFalse();
			response.Errors.Should().BeEmpty();
			response.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetAccountsByIbanNotFound()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid userId = user.Id;
		string iban = string.Empty;

		ErrorOr<AccountResponse> response =
			await _accountService.Get(userId, iban);

		AssertionHelper.AssertInScope(() =>
		{
			response.IsError.Should().BeTrue();
			response.Errors.Should().HaveCount(1);
			response.FirstError.Should().Be(AccountServiceErrors.GetByNumberNotFound(iban));
		});
	}
}