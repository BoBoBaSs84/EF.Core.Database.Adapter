using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Finance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The transaction service class.
/// </summary>
internal sealed class TransactionService : ITransactionService
{
	private readonly ILoggerService<TransactionService> _loggerService;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initilizes an instance of the transaction service class.
	/// </summary>
	/// <param name="loggerService">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public TransactionService(ILoggerService<TransactionService> loggerService, IRepositoryService repositoryService, IMapper mapper)
	{
		_loggerService = loggerService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> CreateForAccount(Guid id, TransactionCreateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			AccountModel? account =
				await _repositoryService.AccountRepository.GetByIdAsync(id, true, true, cancellationToken);

			if (account is null)
				return AccountServiceErrors.GetByIdNotFound(id);

			TransactionModel transaction = _mapper.Map<TransactionModel>(request);

			AccountTransactionModel accountTransaction = new() { Account = account, Transaction = transaction };

			account.AccountTransactions.Add(accountTransaction);

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.CreateForAccountFailed(id);
		}
	}

	public async Task<ErrorOr<Created>> CreateForCard(Guid id, TransactionCreateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			CardModel? card =
				await _repositoryService.CardRepository.GetByIdAsync(id, true, true, cancellationToken);

			if (card is null)
				return CardServiceErrors.GetByIdNotFound(id);

			TransactionModel transaction = _mapper.Map<TransactionModel>(request);

			CardTransactionModel cardTransaction = new() { Card = card, Transaction = transaction };

			card.CardTransactions.Add(cardTransaction);

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.CreateForCardFailed(id);
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid id, CancellationToken cancellationToken = default)
	{
		try
		{
			TransactionModel? transaction =
				await _repositoryService.TransactionRepository.GetByIdAsync(id, true, true, cancellationToken);

			if (transaction is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			await _repositoryService.TransactionRepository.DeleteAsync(transaction);

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.DeleteFailed(id);
		}
	}

	public async Task<ErrorOr<TransactionResponse>> Get(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			TransactionModel? transaction =
				await _repositoryService.TransactionRepository.GetByIdAsync(id, true, false, cancellationToken);

			if (transaction is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			TransactionResponse response = _mapper.Map<TransactionResponse>(transaction);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetForCard(Guid id, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<TransactionModel> entries = await _repositoryService.TransactionRepository.GetManyByConditionAsync(
				expression: x => x.CardTransactions.Select(x => x.CardId).Contains(id),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				orderBy: x => x.OrderBy(x => x.BookingDate),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!entries.Any())
				return TransactionServiceErrors.GetByCardIdNotFound(id);

			int totalCount = await _repositoryService.TransactionRepository.GetCountAsync(
				expression: x => x.CardTransactions.Select(x => x.CardId).Contains(id),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				cancellationToken: cancellationToken
				);

			IEnumerable<TransactionResponse> result = _mapper.Map<IEnumerable<TransactionResponse>>(entries);

			return new PagedList<TransactionResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.GetByCardIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetForAccount(Guid id, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<TransactionModel> entries = await _repositoryService.TransactionRepository.GetManyByConditionAsync(
				expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(id),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				orderBy: x => x.OrderBy(x => x.BookingDate),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!entries.Any())
				return TransactionServiceErrors.GetByAccountIdNotFound(id);

			int totalCount = await _repositoryService.TransactionRepository.GetCountAsync(
				expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(id),
				queryFilter: x => x.FilterByBookingDate(parameters.BookingDate)
				.FilterByValueDate(parameters.ValueDate)
				.FilterByBeneficiary(parameters.Beneficiary)
				.FilterByAmountRange(parameters.MinValue, parameters.MaxValue),
				cancellationToken: cancellationToken
				);

			IEnumerable<TransactionResponse> result = _mapper.Map<IEnumerable<TransactionResponse>>(entries);

			return new PagedList<TransactionResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.GetByAccountIdFailed(id);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid id, TransactionUpdateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			TransactionModel? transaction =
				await _repositoryService.TransactionRepository.GetByIdAsync(id, true, true, cancellationToken);

			if (transaction is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			transaction = _mapper.Map<TransactionModel>(request);

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.UpdateFailed(id);
		}
	}
}
