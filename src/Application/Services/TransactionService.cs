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

	public async Task<ErrorOr<TransactionResponse>> GetById(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			TransactionModel? result = await _repositoryService.TransactionRepository
				.GetByIdAsync(id, true, false, cancellationToken)
				.ConfigureAwait(false);

			if (result is null)
				return TransactionServiceErrors.GetByIdNotFound(id);

			TransactionResponse response = _mapper.Map<TransactionResponse>(result);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return TransactionServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetByCardId(Guid cardId, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<TransactionModel> entries = await _repositoryService.TransactionRepository.GetManyByConditionAsync(
				expression: x => x.CardTransactions.Select(x => x.CardId).Contains(cardId),
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
				return TransactionServiceErrors.GetByCardIdNotFound(cardId);

			int totalCount = await _repositoryService.TransactionRepository.GetCountAsync(
				expression: x => x.CardTransactions.Select(x => x.CardId).Contains(cardId),
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
			return TransactionServiceErrors.GetByCardIdFailed(cardId);
		}
	}

	public async Task<ErrorOr<IPagedList<TransactionResponse>>> GetByAccountId(Guid accountId, TransactionParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<TransactionModel> entries = await _repositoryService.TransactionRepository.GetManyByConditionAsync(
				expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
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
				return TransactionServiceErrors.GetByAccountIdNotFound(accountId);

			int totalCount = await _repositoryService.TransactionRepository.GetCountAsync(
				expression: x => x.AccountTransactions.Select(x => x.AccountId).Contains(accountId),
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
			return TransactionServiceErrors.GetByAccountIdFailed(accountId);
		}
	}
}
