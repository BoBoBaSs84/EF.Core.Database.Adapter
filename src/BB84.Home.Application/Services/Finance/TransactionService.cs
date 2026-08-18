using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Application.Services.Finance;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Finance;

/// <summary>
/// The transaction service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
internal sealed class TransactionService(ILoggerService<TransactionService> loggerService, IRepositoryService repositoryService) : ITransactionService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateByAccountId(Guid accountId, TransactionCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountEntity? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(accountId, cancellationToken: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return TransactionServiceErrors.CreateByAccountIdNotFound(accountId);

			TransactionEntity transaction = request.ToEntity();

			transaction.AccountTransactions = [new() { Account = accountEntity, Transaction = transaction }];

			await repositoryService.TransactionRepository
				.CreateAsync(transaction, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, accountId, ex);
			return TransactionServiceErrors.CreateByAccountIdFailed(accountId);
		}
	}

	public async Task<ErrorOr<Created>> CreateByCardId(Guid cardId, TransactionCreateRequest request, CancellationToken token = default)
	{
		try
		{
			CardEntity? cardEntity = await repositoryService.CardRepository
				.GetByIdAsync(cardId, cancellationToken: token)
				.ConfigureAwait(false);

			if (cardEntity is null)
				return TransactionServiceErrors.CreateByCardIdNotFound(cardId);

			TransactionEntity transaction = request.ToEntity();

			transaction.CardTransactions = [new() { Card = cardEntity, Transaction = transaction }];

			await repositoryService.TransactionRepository
				.CreateAsync(transaction, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, cardId, ex);
			return TransactionServiceErrors.CreateByCardIdFailed(cardId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByAccountId(Guid accountId, Guid id, CancellationToken token = default)
	{
		try
		{
			TransactionEntity? entity = await repositoryService.TransactionRepository
				.GetSingleAsync(new() { Where = x => x.Id.Equals(id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId) }, token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.DeleteByAccountIdNotFound(id);

			repositoryService.TransactionRepository
				.Delete(entity);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{accountId}", $"{id}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.DeleteByAccountIdFailed(id);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByCardId(Guid cardId, Guid id, CancellationToken token = default)
	{
		try
		{
			TransactionEntity? entity = await repositoryService.TransactionRepository
				.GetSingleAsync(new() { Where = x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId) }, token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.DeleteByCardIdNotFound(id);

			repositoryService.TransactionRepository
				.Delete(entity);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{cardId}", $"{id}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.DeleteByCardIdFailed(id);
		}
	}

	public async Task<ErrorOr<TransactionResponse>> GetByAccountId(Guid accountId, Guid id, CancellationToken token = default)
	{
		try
		{
			TransactionEntity? entity = await repositoryService.TransactionRepository
				.GetSingleAsync(new() { Where = x => x.Id.Equals(id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId) }, token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			TransactionResponse response = entity.ToResponse();

			return response;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{accountId}", $"{id}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<TransactionResponse>> GetByCardId(Guid cardId, Guid id, CancellationToken token = default)
	{
		try
		{
			TransactionEntity? entity = await repositoryService.TransactionRepository
				.GetSingleAsync(new() { Where = x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId) }, token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			TransactionResponse response = entity.ToResponse();

			return response;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{cardId}", $"{id}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetPagedByAccountId(Guid id, TransactionParameters parameters, CancellationToken token = default)
	{
		try
		{
			Query<TransactionEntity> listQuery = new()
			{
				Where = x => x.AccountTransactions.Select(x => x.AccountId).Contains(id),
				QueryFilter = x => x.FilterByParameters(parameters),
				OrderBy = x => x.OrderBy(x => x.BookingDate),
				Skip = (parameters.PageNumber - 1) * parameters.PageSize,
				Take = parameters.PageSize
			};

			IReadOnlyList<TransactionEntity> entities = await repositoryService.TransactionRepository
				.GetListAsync(listQuery, token)
				.ConfigureAwait(false);

			Query<TransactionEntity> countQuery = new()
			{
				Where = x => x.AccountTransactions.Select(x => x.AccountId).Contains(id),
				QueryFilter = x => x.FilterByParameters(parameters)
			};

			int totalCount = await repositoryService.TransactionRepository
				.CountAsync(countQuery, token)
				.ConfigureAwait(false);

			IEnumerable<TransactionResponse> result = entities.Select(x => x.ToResponse());

			return new PagedList<TransactionResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return TransactionServiceErrors.GetPagedByAccountIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetPagedByCardId(Guid id, TransactionParameters parameters, CancellationToken token = default)
	{
		try
		{
			Query<TransactionEntity> listQuery = new()
			{
				Where = x => x.CardTransactions.Select(x => x.CardId).Contains(id),
				QueryFilter = x => x.FilterByParameters(parameters),
				OrderBy = x => x.OrderBy(x => x.BookingDate),
				Skip = (parameters.PageNumber - 1) * parameters.PageSize,
				Take = parameters.PageSize
			};

			IReadOnlyList<TransactionEntity> entities = await repositoryService.TransactionRepository
				.GetListAsync(listQuery, token)
				.ConfigureAwait(false);

			Query<TransactionEntity> countQuery = new()
			{
				Where = x => x.CardTransactions.Select(x => x.CardId).Contains(id),
				QueryFilter = x => x.FilterByParameters(parameters)
			};

			int totalCount = await repositoryService.TransactionRepository
				.CountAsync(countQuery, token)
				.ConfigureAwait(false);

			IEnumerable<TransactionResponse> result = entities.Select(x => x.ToResponse());

			return new PagedList<TransactionResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return TransactionServiceErrors.GetPagedByCardIdFailed(id);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateByAccountId(Guid accountId, Guid id, TransactionUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			Query<TransactionEntity> query = new()
			{
				Where = x => x.Id.Equals(id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
				TrackChanges = true
			};

			TransactionEntity? entity = await repositoryService.TransactionRepository
				.GetSingleAsync(query, token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.UpdateByAccountIdNotFound(id);

			entity = request.ToEntity(entity);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{accountId}", $"{id}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.UpdateByAccountIdFailed(id);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateByCardId(Guid cardId, Guid id, TransactionUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			Query<TransactionEntity> query = new()
			{
				Where = x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId),
				TrackChanges = true
			};

			TransactionEntity? entity = await repositoryService.TransactionRepository
				.GetSingleAsync(query, token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.UpdateByCardIdNotFound(id);

			entity = request.ToEntity(entity);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{cardId}", $"{id}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.UpdateByCardIdFailed(id);
		}
	}
}
