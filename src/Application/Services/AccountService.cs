using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Models.Finance;
using Domain.Models.Identity;
using Domain.Results;
using Domain.Statics;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The account service class.
/// </summary>
internal sealed class AccountService : IAccountService
{
	private readonly ILoggerService<AccountService> _loggerService;
	private readonly IUserService _userService;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the account service class.
	/// </summary>
	/// <param name="loggerService">The logger service to use.</param>
	/// <param name="userService">The user service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public AccountService(ILoggerService<AccountService> loggerService, IUserService userService, IRepositoryService repositoryService, IMapper mapper)
	{
		_loggerService = loggerService;
		_userService = userService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

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

			UserModel userModel = await _userService.FindByIdAsync($"{userId}");
			AccountModel newAccount = _mapper.Map<AccountModel>(request);

			if (newAccount.Cards.Count > 0)
			{
				foreach (CardModel card in newAccount.Cards)
					card.User = userModel;
			}

			AccountUserModel newAccountUser = new() { User = userModel, Account = newAccount };
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
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
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
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
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
		string[] parameters = new string[] { $"{userId}", $"{iban}" };
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId) && x.IBAN == iban && x.Cards.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AccountModel.Cards) }
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
		ErrorOr<Updated> response = new();
		try
		{
			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AccountModel.Cards) }
				);

			if (accountEntry is null)
				response.Errors.Add(AccountServiceErrors.UpdateAccountNotFound(request.Id));

			if (request.Cards is not null)
				foreach (CardUpdateRequest cardRequest in request.Cards)
				{
					CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
						expression: x => x.Id.Equals(cardRequest.Id) && x.UserId.Equals(userId) && x.AccountId.Equals(request.Id),
						trackChanges: false,
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

	private static void UpdateAccount(AccountModel account, AccountUpdateRequest updateRequest)
	{
		account.Provider = updateRequest.Provider;

		if (account.Cards.Any() && updateRequest.Cards is not null && updateRequest.Cards.Any())
			foreach (CardModel card in account.Cards)
				card.ValidUntil = updateRequest.Cards.Where(x => x.Id.Equals(card.Id)).Select(x => x.ValidUntil).First();
	}
}
