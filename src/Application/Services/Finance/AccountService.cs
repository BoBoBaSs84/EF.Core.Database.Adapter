using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Extensions;

using Domain.Errors;
using Domain.Models.Finance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services.Finance;

/// <summary>
/// The account service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
/// <param name="mapper">The auto mapper to use.</param>
internal sealed class AccountService(ILoggerService<AccountService> loggerService, IRepositoryService repositoryService, IMapper mapper) : IAccountService
{
	private readonly ILoggerService<AccountService> _loggerService = loggerService;
	private readonly IRepositoryService _repositoryService = repositoryService;
	private readonly IMapper _mapper = mapper;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> Create(Guid userId, AccountCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntity = await _repositoryService.AccountRepository
				.GetByConditionAsync(expression: x => x.IBAN == request.IBAN, token: token)
				.ConfigureAwait(false);

			if (accountEntity is not null)
				return AccountServiceErrors.CreateAccountNumberConflict(request.IBAN);

			if (request.Cards is not null)
			{
				foreach (CardCreateRequest cardRequest in request.Cards)
				{
					CardModel? cardEntity = await _repositoryService.CardRepository
						.GetByConditionAsync(expression: x => x.PAN == cardRequest.PAN, token: token)
						.ConfigureAwait(false);

					if (cardEntity is not null)
						return AccountServiceErrors.CreateCardNumberConflict(cardRequest.PAN);
				}
			}

			AccountModel newAccount = _mapper.Map<AccountModel>(request);

			if (newAccount.Cards.Count > 0)
			{
				foreach (CardModel card in newAccount.Cards)
					card.UserId = userId;
			}

			newAccount.AccountUsers.Add(new() { UserId = userId, Account = newAccount });

			await _repositoryService.AccountRepository.CreateAsync(newAccount, token)
				.ConfigureAwait(false);

			_ = await _repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AccountServiceErrors.CreateAccountFailed(request.IBAN);
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid accountId, CancellationToken token = default)
	{
		try
		{
			int result = await _repositoryService.AccountRepository
				.DeleteAsync(accountId, token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? AccountServiceErrors.DeleteAccountNotFound(accountId)
				: Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, accountId, ex);
			return AccountServiceErrors.DeleteAccountFailed(accountId);
		}
	}

	public async Task<ErrorOr<IEnumerable<AccountResponse>>> GetByUserId(Guid id, CancellationToken token = default)
	{
		try
		{
			IEnumerable<AccountModel> accountEntities = await _repositoryService.AccountRepository
				.GetManyByConditionAsync(
					expression: x => x.AccountUsers.Select(x => x.UserId).Contains(id),
					token: token)
				.ConfigureAwait(false);

			IEnumerable<CardModel> cardEntities = await _repositoryService.CardRepository
				.GetManyByConditionAsync(
					expression: x => x.UserId.Equals(id) && accountEntities.Select(x => x.Id).Contains(x.AccountId),
					token: token)
				.ConfigureAwait(false);

			if (cardEntities.Any().IsTrue())
				foreach (AccountModel account in accountEntities)
					account.Cards = cardEntities.Where(x => x.AccountId.Equals(account.Id)).ToList();

			IEnumerable<AccountResponse> result = _mapper.Map<IEnumerable<AccountResponse>>(accountEntities);

			return result.ToList();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return AccountServiceErrors.GetByUserId;
		}
	}

	public async Task<ErrorOr<AccountResponse>> GetByAccountId(Guid id, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository
				.GetByConditionAsync(
					expression: x => x.Id.Equals(id) && x.AccountUsers.Select(x => x.UserId).Contains(id),
					token: token)
				.ConfigureAwait(false);

			if (accountEntry is null)
				return AccountServiceErrors.GetByIdNotFound(id);

			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository
				.GetManyByConditionAsync(
					expression: x => x.UserId.Equals(id) && x.AccountId.Equals(id),
					token: token)
				.ConfigureAwait(false);

			if (cardEntries.Any().Equals(true))
				accountEntry.Cards = cardEntries.ToList();

			AccountResponse response = _mapper.Map<AccountResponse>(accountEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return AccountServiceErrors.GetByAccountId(id);
		}
	}

	public async Task<ErrorOr<AccountResponse>> GetByNumber(Guid userId, string iban, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository
				.GetByConditionAsync(
					expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId) && x.IBAN == iban && x.Cards.Select(x => x.UserId).Contains(userId),
					token: token,
					includeProperties: [nameof(AccountModel.Cards)])
				.ConfigureAwait(false);

			if (accountEntry is null)
				return AccountServiceErrors.GetByNumberNotFound(iban);

			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository
				.GetManyByConditionAsync(
					expression: x => x.UserId.Equals(userId) && x.AccountId.Equals(accountEntry.Id),
					token: token)
				.ConfigureAwait(false);

			if (cardEntries.Any().Equals(true))
				accountEntry.Cards = cardEntries.ToList();

			AccountResponse response = _mapper.Map<AccountResponse>(accountEntry);

			return response;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{iban}"];
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByNumberFailed(iban);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, AccountUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			ErrorOr<Updated> response = new();

			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				token: token,
				includeProperties: [nameof(AccountModel.Cards)]
				);

			if (accountEntry is null)
				response.Errors.Add(AccountServiceErrors.UpdateAccountNotFound(request.Id));

			if (request.Cards is not null)
				foreach (CardUpdateRequest cardRequest in request.Cards)
				{
					CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
						expression: x => x.Id.Equals(cardRequest.Id) && x.UserId.Equals(userId) && x.AccountId.Equals(request.Id),
						trackChanges: true,
						token: token
						);

					if (cardEntry is null)
						response.Errors.Add(AccountServiceErrors.UpdateCardNotFound(cardRequest.Id));
				}

			if (response.IsError)
				return response;

			if (accountEntry is not null)
				UpdateAccount(accountEntry, request);

			_ = await _repositoryService.CommitChangesAsync(token);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AccountServiceErrors.UpdateAccountFailed;
		}
	}

	private static void UpdateAccount(AccountModel account, AccountUpdateRequest request)
	{
		account.Provider = request.Provider;

		if (request.Cards is not null && request.Cards.Length > 0)
		{
			foreach (CardUpdateRequest cardRequest in request.Cards)
			{
				CardModel? card = account.Cards.Where(x => x.Id.Equals(cardRequest.Id)).FirstOrDefault();

				if (card is not null)
					card.ValidUntil = cardRequest.ValidUntil;
			}
		}
	}
}
