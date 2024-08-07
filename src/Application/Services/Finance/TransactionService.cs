﻿using Application.Contracts.Requests.Finance;
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
	private readonly ILoggerService<TransactionService> _loggerService = loggerService;
	private readonly IRepositoryService _repositoryService = repositoryService;
	private readonly IMapper _mapper = mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateByAccountId(Guid accountId, TransactionCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntry =
				await _repositoryService.AccountRepository.GetByIdAsync(accountId, true, true, token);

			if (accountEntry is null)
				return AccountServiceErrors.GetByIdNotFound(accountId);

			TransactionModel newTransaction = _mapper.Map<TransactionModel>(request);

			AccountTransactionModel accountTransaction = new() { Account = accountEntry, Transaction = newTransaction };

			accountEntry.Transactions.Add(accountTransaction);

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.CreateForAccountFailed(accountId);
		}
	}

	public async Task<ErrorOr<Created>> CreateByCardId(Guid cardId, TransactionCreateRequest request, CancellationToken token = default)
	{
		try
		{
			CardModel? cardEntry =
				await _repositoryService.CardRepository.GetByIdAsync(cardId, true, true, token);

			if (cardEntry is null)
				return CardServiceErrors.GetByIdNotFound(cardId);

			TransactionModel newTransaction = _mapper.Map<TransactionModel>(request);

			CardTransactionModel cardTransaction = new() { Card = cardEntry, Transaction = newTransaction };

			cardEntry.Transactions.Add(cardTransaction);

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.CreateForCardFailed(cardId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByAccountId(Guid accountId, Guid transactionId, CancellationToken token = default)
	{
		string[] parameters = [$"{accountId}", $"{transactionId}"];
		try
		{
			TransactionModel? transactionEntry = await _repositoryService.TransactionRepository.GetByConditionAsync(
					expression: x => x.Id.Equals(transactionId) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
					trackChanges: true,
					token: token
					);

			if (transactionEntry is null)
				return TransactionServiceErrors.GetByIdNotFound(transactionId);

			await _repositoryService.TransactionRepository.DeleteAsync(transactionEntry);

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.DeleteFailed(transactionId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByCardId(Guid cardId, Guid transactionId, CancellationToken token = default)
	{
		string[] parameters = [$"{cardId}", $"{transactionId}"];
		try
		{
			TransactionModel? transactionEntry = await _repositoryService.TransactionRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(transactionId) && x.CardTransactions.Select(x => x.CardId).Contains(cardId),
				trackChanges: true,
				token: token
				);

			if (transactionEntry is null)
				return TransactionServiceErrors.GetByIdNotFound(transactionId);

			await _repositoryService.TransactionRepository.DeleteAsync(transactionEntry);

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.DeleteFailed(transactionId);
		}
	}

	public async Task<ErrorOr<TransactionResponse>> GetByAccountId(Guid accountId, Guid transactionId, bool trackChanges = false, CancellationToken token = default)
	{
		string[] parameters = [$"{accountId}", $"{transactionId}"];
		try
		{
			TransactionModel? transactionEntry = await _repositoryService.TransactionRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(transactionId) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
				trackChanges: trackChanges,
				token: token
				);

			if (transactionEntry is null)
				return TransactionServiceErrors.GetByIdNotFound(transactionId);

			TransactionResponse response = _mapper.Map<TransactionResponse>(transactionEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.GetByIdFailed(transactionId);
		}
	}

	public async Task<ErrorOr<TransactionResponse>> GetByCardId(Guid cardId, Guid transactionId, bool trackChanges = false, CancellationToken token = default)
	{
		string[] parameters = [$"{cardId}", $"{transactionId}"];
		try
		{
			TransactionModel? transactionEntry = await _repositoryService.TransactionRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(transactionId) && x.CardTransactions.Select(x => x.CardId).Contains(cardId),
				trackChanges: trackChanges,
				token: token
				);

			if (transactionEntry is null)
				return TransactionServiceErrors.GetByIdNotFound(transactionId);

			TransactionResponse response = _mapper.Map<TransactionResponse>(transactionEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.GetByIdFailed(transactionId);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetByAccountId(Guid accountId, TransactionParameters parameters, bool trackChanges = false, CancellationToken token = default)
	{
		try
		{
			IEnumerable<TransactionModel> transactionEntries = await _repositoryService.TransactionRepository.GetManyByConditionAsync(
				expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				orderBy: x => x.OrderBy(x => x.BookingDate),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				token: token
				);

			if (transactionEntries.Any().Equals(false))
				return TransactionServiceErrors.GetByAccountIdNotFound(accountId);

			int totalCount = await _repositoryService.TransactionRepository.CountAsync(
				expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				token: token
				);

			IEnumerable<TransactionResponse> result = _mapper.Map<IEnumerable<TransactionResponse>>(transactionEntries);

			return new PagedList<TransactionResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.GetByAccountIdFailed(accountId);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetByCardId(Guid cardId, TransactionParameters parameters, bool trackChanges = false, CancellationToken token = default)
	{
		try
		{
			IEnumerable<TransactionModel> transactionEntries = await _repositoryService.TransactionRepository.GetManyByConditionAsync(
				expression: x => x.CardTransactions.Select(x => x.CardId).Contains(cardId),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				orderBy: x => x.OrderBy(x => x.BookingDate),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				token: token
				);

			if (transactionEntries.Any().Equals(false))
				return TransactionServiceErrors.GetByCardIdNotFound(cardId);

			int totalCount = await _repositoryService.TransactionRepository.CountAsync(
				expression: x => x.CardTransactions.Select(x => x.CardId).Contains(cardId),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				token: token
				);

			IEnumerable<TransactionResponse> result = _mapper.Map<IEnumerable<TransactionResponse>>(transactionEntries);

			return new PagedList<TransactionResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.GetByCardIdFailed(cardId);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateByAccountId(Guid accountId, TransactionUpdateRequest request, CancellationToken token = default)
	{
		string[] parameters = [$"{accountId}", $"{request.Id}"];
		try
		{
			TransactionModel? transactionEntry = await _repositoryService.TransactionRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id) && x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
				trackChanges: true,
				token: token
				);

			if (transactionEntry is null)
				return TransactionServiceErrors.GetByIdNotFound(request.Id);

			transactionEntry = _mapper.Map<TransactionModel>(request);

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.UpdateFailed(request.Id);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateByCardId(Guid cardId, TransactionUpdateRequest request, CancellationToken token = default)
	{
		string[] parameters = [$"{cardId}", $"{request.Id}"];
		try
		{
			TransactionModel? transactionEntry = await _repositoryService.TransactionRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id) && x.CardTransactions.Select(x => x.CardId).Contains(cardId),
				trackChanges: true,
				token: token
				);

			if (transactionEntry is null)
				return TransactionServiceErrors.GetByIdNotFound(request.Id);

			transactionEntry = _mapper.Map<TransactionModel>(request);

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return TransactionServiceErrors.UpdateFailed(request.Id);
		}
	}
}
