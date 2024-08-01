using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Common;
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

	public async Task<ErrorOr<Created>> Create(Guid userId, AccountCreateRequest request, CancellationToken cancellationToken = default)
	{
		ErrorOr<Created> response = new();
		try
		{
			if (RegexStatics.Iban.IsMatch(request.IBAN).Equals(false))
			{
				response.Errors.Add(AccountServiceErrors.CreateAccountNumberInvalid(request.IBAN));
				return response;
			}

			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.IBAN == request.IBAN,
				cancellationToken: cancellationToken
				);

			if (accountEntry is not null)
				response.Errors.Add(AccountServiceErrors.CreateAccountNumberConflict(accountEntry.IBAN));

			if (request.Cards is not null)
				foreach (CardCreateRequest cardRequest in request.Cards)
				{
					if (RegexStatics.CreditCard.IsMatch(cardRequest.PAN).Equals(false))
					{
						response.Errors.Add(AccountServiceErrors.CreateCardNumberInvalid(cardRequest.PAN));
						continue;
					}

					CardModel? cardModel = await _repositoryService.CardRepository.GetByConditionAsync(
						expression: x => x.PAN == cardRequest.PAN,
						cancellationToken: cancellationToken
						);

					if (cardModel is not null)
						response.Errors.Add(AccountServiceErrors.CreateCardNumberConflict(cardModel.PAN));
				}

			if (response.IsError)
				return response;

			AccountModel newAccount = _mapper.Map<AccountModel>(request);

			if (newAccount.Cards.Count > 0)
			{
				foreach (CardModel card in newAccount.Cards)
					card.UserId = userId;
			}

			AccountUserModel newAccountUser = new() { UserId = userId, Account = newAccount };
			newAccount.AccountUsers.Add(newAccountUser);

			await _repositoryService.AccountRepository.CreateAsync(newAccount, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AccountServiceErrors.CreateAccountFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, Guid accountId, CancellationToken cancellationToken = default)
	{
		string[] parameters = [$"{userId}", $"{accountId}"];
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (accountEntry is null)
				return AccountServiceErrors.DeleteAccountNotFound(accountId);

			await _repositoryService.AccountRepository.DeleteAsync(accountEntry);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AccountServiceErrors.DeleteAccountFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<AccountResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<AccountModel> accountEntries = await _repositoryService.AccountRepository.GetManyByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (accountEntries.Any().Equals(false))
				return AccountServiceErrors.GetAllNotFound;

			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId) && accountEntries.Select(x => x.Id).Contains(x.AccountId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (cardEntries.Any().Equals(true))
				foreach (AccountModel account in accountEntries)
					account.Cards = cardEntries.Where(x => x.AccountId.Equals(account.Id)).ToList();

			IEnumerable<AccountResponse> result = _mapper.Map<IEnumerable<AccountResponse>>(accountEntries);

			return result.ToList();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, userId, ex);
			return AccountServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<AccountResponse>> Get(Guid userId, Guid accountId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = [$"{userId}", $"{accountId}"];
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (accountEntry is null)
				return AccountServiceErrors.GetByIdNotFound(accountId);

			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.AccountId.Equals(accountId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (cardEntries.Any().Equals(true))
				accountEntry.Cards = cardEntries.ToList();

			AccountResponse response = _mapper.Map<AccountResponse>(accountEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByIdFailed(accountId);
		}
	}

	public async Task<ErrorOr<AccountResponse>> Get(Guid userId, string iban, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = [$"{userId}", $"{iban}"];
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId) && x.IBAN == iban && x.Cards.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: [nameof(AccountModel.Cards)]
				);

			if (accountEntry is null)
				return AccountServiceErrors.GetByNumberNotFound(iban);

			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.AccountId.Equals(accountEntry.Id),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (cardEntries.Any().Equals(true))
				accountEntry.Cards = cardEntries.ToList();

			AccountResponse response = _mapper.Map<AccountResponse>(accountEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByNumberFailed(iban);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, AccountUpdateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			ErrorOr<Updated> response = new();

			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken,
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
						cancellationToken: cancellationToken
						);

					if (cardEntry is null)
						response.Errors.Add(AccountServiceErrors.UpdateCardNotFound(cardRequest.Id));
				}

			if (response.IsError)
				return response;

			if (accountEntry is not null)
				UpdateAccount(accountEntry, request);

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

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
