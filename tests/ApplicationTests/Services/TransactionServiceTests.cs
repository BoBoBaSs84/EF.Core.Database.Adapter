using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Finance;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public sealed class TransactionServiceTests : ApplicationTestBase
{
	private readonly ITransactionService _transactionService;

	public TransactionServiceTests()
		=> _transactionService = GetService<ITransactionService>();

	[TestMethod]
	public async Task CreateByAccountIdNotFound()
	{
		Guid accountId = default;
		TransactionCreateRequest request = new();

		ErrorOr<Created> result =
			await _transactionService.CreateByAccountId(accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AccountServiceErrors.GetByIdNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task CreateByAccountIdSuccess()
	{
		(Guid accountId, _) = GetAccountTransaction();
		TransactionCreateRequest request = new();

		ErrorOr<Created> result =
			await _transactionService.CreateByAccountId(accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task CreateByCardIdNotFound()
	{
		Guid cardId = default;
		TransactionCreateRequest request = new();

		ErrorOr<Created> result =
			await _transactionService.CreateByCardId(cardId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CardServiceErrors.GetByIdNotFound(cardId));
		});
	}

	[TestMethod]
	public async Task CreateByCardIdSuccess()
	{
		(Guid cardId, _) = GetCardTransaction();
		TransactionCreateRequest request = new();

		ErrorOr<Created> result =
			await _transactionService.CreateByCardId(cardId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task DeleteByAccountIdNotFound()
	{
		(Guid accountId, Guid transactionId) = GetAccountTransaction();
		transactionId = default;

		ErrorOr<Deleted> result =
			await _transactionService.DeleteByAccountId(accountId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByIdNotFound(transactionId));
		});
	}

	[TestMethod]
	public async Task DeleteByAccountIdSuccess()
	{
		(Guid accountId, Guid transactionId) = GetAccountTransaction();

		ErrorOr<Deleted> result =
			await _transactionService.DeleteByAccountId(accountId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task DeleteByCardIdNotFound()
	{
		(Guid cardId, Guid transactionId) = GetCardTransaction();
		transactionId = default;

		ErrorOr<Deleted> result =
			await _transactionService.DeleteByCardId(cardId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByIdNotFound(transactionId));
		});
	}

	[TestMethod]
	public async Task DeleteByCardIdSuccess()
	{
		(Guid cardId, Guid transactionId) = GetCardTransaction();

		ErrorOr<Deleted> result =
			await _transactionService.DeleteByCardId(cardId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task GetByAccountIdNotFound()
	{
		(Guid accountId, Guid transactionId) = GetAccountTransaction();
		transactionId = default;

		ErrorOr<TransactionResponse> result =
			await _transactionService.GetByAccountId(accountId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByIdNotFound(transactionId));
		});
	}

	[TestMethod]
	public async Task GetByAccountIdSuccess()
	{
		(Guid accountId, Guid transactionId) = GetAccountTransaction();

		ErrorOr<TransactionResponse> result =
			await _transactionService.GetByAccountId(accountId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetPagedByAccountIdNotFound()
	{
		(Guid accountId, _) = GetAccountTransaction();
		TransactionParameters parameters = new() { Beneficiary = "UnitTest" };

		ErrorOr<IPagedList<TransactionResponse>> result =
			await _transactionService.GetByAccountId(accountId, parameters);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByAccountIdNotFound(accountId));
		});
	}

	[TestMethod]
	public async Task GetPagedByAccountIdSuccess()
	{
		(Guid accountId, _) = GetAccountTransaction();
		TransactionParameters parameters = new();

		ErrorOr<IPagedList<TransactionResponse>> result =
			await _transactionService.GetByAccountId(accountId, parameters);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	public async Task GetByCardIdNotFound()
	{
		(Guid cardId, Guid transactionId) = GetCardTransaction();
		transactionId = default;

		ErrorOr<TransactionResponse> result =
			await _transactionService.GetByCardId(cardId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByIdNotFound(transactionId));
		});
	}

	[TestMethod]
	public async Task GetByCardIdSuccess()
	{
		(Guid cardId, Guid transactionId) = GetCardTransaction();

		ErrorOr<TransactionResponse> result =
			await _transactionService.GetByCardId(cardId, transactionId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetPagedByCardIdNotFound()
	{
		(Guid cardId, _) = GetCardTransaction();
		TransactionParameters parameters = new() { Beneficiary = "UnitTest" };

		ErrorOr<IPagedList<TransactionResponse>> result =
			await _transactionService.GetByCardId(cardId, parameters);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByCardIdNotFound(cardId));
		});
	}

	[TestMethod]
	public async Task GetPagedByCardIdSuccess()
	{
		(Guid cardId, _) = GetCardTransaction();
		TransactionParameters parameters = new();

		ErrorOr<IPagedList<TransactionResponse>> result =
			await _transactionService.GetByCardId(cardId, parameters);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	public async Task UpdateByAccountIdNotFound()
	{
		(Guid accountId, _) = GetAccountTransaction();
		Guid transactionId = default;
		TransactionUpdateRequest request = new() { Id = transactionId };

		ErrorOr<Updated> result =
			await _transactionService.UpdateByAccountId(accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByIdNotFound(transactionId));
		});
	}

	[TestMethod]
	public async Task UpdateByAccountIdSuccess()
	{
		(Guid accountId, Guid transactionId) = GetAccountTransaction();
		TransactionUpdateRequest request = new() { Id = transactionId };

		ErrorOr<Updated> result =
			await _transactionService.UpdateByAccountId(accountId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod]
	public async Task UpdateByCardIdNotFound()
	{
		(Guid cardId, _) = GetCardTransaction();
		Guid transactionId = default;
		TransactionUpdateRequest request = new() { Id = transactionId };

		ErrorOr<Updated> result =
			await _transactionService.UpdateByCardId(cardId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(TransactionServiceErrors.GetByIdNotFound(transactionId));
		});
	}

	[TestMethod]
	public async Task UpdateByCardIdSuccess()
	{
		(Guid cardId, Guid transactionId) = GetCardTransaction();
		TransactionUpdateRequest request = new() { Id = transactionId };

		ErrorOr<Updated> result =
			await _transactionService.UpdateByCardId(cardId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	private static (Guid AccountId, Guid TransactionId) GetAccountTransaction()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid accountId = user.AccountUsers
			.Select(x => x.AccountId)
			.ToList()[RandomHelper.GetInt(0, user.AccountUsers.Count)];
		ICollection<AccountTransactionModel> transactions = user.AccountUsers
			.Select(x => x.Account)
			.Where(x => x.Id.Equals(accountId)).First().AccountTransactions;
		Guid transactionId = transactions
			.Select(x => x.TransactionId)
			.ToList()[RandomHelper.GetInt(0, transactions.Count)];

		return (accountId, transactionId);
	}

	private static (Guid CardId, Guid TransactionId) GetCardTransaction()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];
		Guid cardId = user.Cards
			.Select(x => x.Id)
			.ToList()[RandomHelper.GetInt(0, user.Cards.Count)];
		ICollection<CardTransactionModel> transactions = user.Cards
			.Where(x => x.Id.Equals(cardId))
			.First().CardTransactions;
		Guid transactionId = transactions
			.Select(x => x.TransactionId)
			.ToList()[RandomHelper.GetInt(0, transactions.Count)];

		return (cardId, transactionId);
	}
}