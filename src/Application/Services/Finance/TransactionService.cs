using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Extensions;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Models.Finance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services.Finance;

/// <summary>
/// The transaction service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
/// <param name="mapper">The auto mapper to use.</param>
internal sealed class TransactionService(ILoggerService<TransactionService> loggerService, IRepositoryService repositoryService, IMapper mapper) : ITransactionService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateByAccountId(Guid accountId, TransactionCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(accountId, token: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return TransactionServiceErrors.CreateByAccountIdNotFound(accountId);

			TransactionModel transaction = mapper.Map<TransactionModel>(request);

			transaction.AccountTransactions = [new() { Account = accountEntity, Transaction = transaction }];

			await repositoryService.TransactionRepository.CreateAsync(transaction, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
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
			CardModel? cardEntity = await repositoryService.CardRepository
				.GetByIdAsync(cardId, token: token)
				.ConfigureAwait(false);

			if (cardEntity is null)
				return TransactionServiceErrors.CreateByCardIdNotFound(cardId);

			TransactionModel transaction = mapper.Map<TransactionModel>(request);

			transaction.CardTransactions = [new() { Card = cardEntity, Transaction = transaction }];

			await repositoryService.TransactionRepository.CreateAsync(transaction, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
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
			TransactionModel? entity = await repositoryService.TransactionRepository
				.GetByConditionAsync(x => x.Id.Equals(id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId), token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.DeleteByAccountIdNotFound(id);

			await repositoryService.TransactionRepository
				.DeleteAsync(entity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
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
			TransactionModel? entity = await repositoryService.TransactionRepository
				.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.DeleteByCardIdNotFound(id);

			await repositoryService.TransactionRepository
				.DeleteAsync(entity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
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
			TransactionModel? entity = await repositoryService.TransactionRepository
				.GetByConditionAsync(x => x.Id.Equals(id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId), token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			TransactionResponse response = mapper.Map<TransactionResponse>(entity);

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
			TransactionModel? entity = await repositoryService.TransactionRepository
				.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			TransactionResponse response = mapper.Map<TransactionResponse>(entity);

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
			IEnumerable<TransactionModel> entities = await repositoryService.TransactionRepository
				.GetManyByConditionAsync(
					expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(id),
					queryFilter: x => x.FilterByParameters(parameters),
					orderBy: x => x.OrderBy(x => x.BookingDate),
					skip: (parameters.PageNumber - 1) * parameters.PageSize,
					take: parameters.PageSize,
					token: token)
				.ConfigureAwait(false);

			int totalCount = await repositoryService.TransactionRepository
				.CountAsync(
					expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(id),
					queryFilter: x => x.FilterByParameters(parameters),
					token: token)
				.ConfigureAwait(false);

			IEnumerable<TransactionResponse> result = mapper.Map<IEnumerable<TransactionResponse>>(entities);

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
			IEnumerable<TransactionModel> entities = await repositoryService.TransactionRepository
				.GetManyByConditionAsync(
					expression: x => x.CardTransactions.Select(x => x.CardId).Contains(id),
					queryFilter: x => x.FilterByParameters(parameters),
					orderBy: x => x.OrderBy(x => x.BookingDate),
					skip: (parameters.PageNumber - 1) * parameters.PageSize,
					take: parameters.PageSize,
					token: token)
				.ConfigureAwait(false);

			int totalCount = await repositoryService.TransactionRepository
				.CountAsync(
					expression: x => x.CardTransactions.Select(x => x.CardId).Contains(id),
					queryFilter: x => x.FilterByParameters(parameters),
					token: token)
				.ConfigureAwait(false);

			IEnumerable<TransactionResponse> result = mapper.Map<IEnumerable<TransactionResponse>>(entities);

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
			TransactionModel? entity = await repositoryService.TransactionRepository
				.GetByConditionAsync(x => x.Id.Equals(id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId), trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.UpdateByAccountIdNotFound(id);

			_ = mapper.Map(request, entity);

			_ = await repositoryService.CommitChangesAsync(token)
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
			TransactionModel? entity = await repositoryService.TransactionRepository
				.GetByConditionAsync(x => x.Id.Equals(id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId), trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return TransactionServiceErrors.UpdateByCardIdNotFound(id);

			_ = mapper.Map(request, entity);

			_ = await repositoryService.CommitChangesAsync(token)
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
