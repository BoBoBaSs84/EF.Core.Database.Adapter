using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Models.Finance;
using Domain.Models.Identity;
using Domain.Errors;
using Domain.Results;

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

	public async Task<ErrorOr<Created>> Create(Guid userId, AccountCreateRequest createRequest, CancellationToken cancellationToken = default)
	{
		ErrorOr<Created> response = new();
		try
		{
			AccountModel? dbAccount = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.IBAN == createRequest.IBAN,
				cancellationToken: cancellationToken
				);

			if (dbAccount is not null)
				response.Errors.Add(AccountServiceErrors.CreateAccountNumberConflict(dbAccount.IBAN));

			if (createRequest.Cards is not null)
				foreach (CardCreateRequest card in createRequest.Cards)
				{
					CardModel? dbCard = await _repositoryService.CardRepository.GetByConditionAsync(
						expression: x => x.PAN == card.PAN,
						trackChanges: false,
						cancellationToken: cancellationToken
						);

					if (dbCard is not null)
						response.Errors.Add(AccountServiceErrors.CreateCardNumberConflict(dbCard.PAN));
				}

			if (response.IsError)
				return response;

			UserModel dbUser = await _userService.FindByIdAsync($"{userId}");
			AccountModel newAccount = _mapper.Map<AccountModel>(createRequest);

			if (newAccount.Cards is not null)
				foreach (CardModel card in newAccount.Cards)
					card.User = dbUser;

			AccountUserModel newAccountUser = new() { User = dbUser, Account = newAccount };
			newAccount.AccountUsers.Add(newAccountUser);

			await _repositoryService.AccountRepository.CreateAsync(newAccount, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, createRequest, ex);
			return AccountServiceErrors.CreateAccountFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, Guid accountId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
		try
		{
			AccountModel? account = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AccountModel.AccountUsers), nameof(AccountModel.Cards), nameof(AccountModel.AccountTransactions) }
				);

			if (account is null)
				return AccountServiceErrors.DeleteAccountNotFound(accountId);

			await _repositoryService.AccountRepository.DeleteAsync(account);
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
		// TODO: figure out how this would work...
		//var qry = Foo.GroupJoin(
		//					Bar,
		//					foo => foo.Foo_Id,
		//					bar => bar.Foo_Id,
		//					(x, y) => new { Foo = x, Bars = y })
		//			 .SelectMany(
		//					 x => x.Bars.DefaultIfEmpty(),
		//					 (x, y) => new { Foo = x.Foo, Bar = y });
		try
		{
			IEnumerable<AccountModel> accounts = await _repositoryService.AccountRepository.GetManyByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!accounts.Any())
				return AccountServiceErrors.GetAllNotFound;

			IEnumerable<CardModel> cards = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(CardModel.CardType) }
				);

			foreach (AccountModel account in accounts)
				account.Cards = cards.Where(x => x.AccountId.Equals(account.Id)).ToList();

			IEnumerable<AccountResponse> result = _mapper.Map<IEnumerable<AccountResponse>>(accounts);

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
			AccountModel? account = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AccountModel.Cards) }
				);

			if (account is null)
				return AccountServiceErrors.GetByIdNotFound(accountId);

			AccountResponse response = _mapper.Map<AccountResponse>(account);

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
			AccountModel? account = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId) && x.IBAN == iban,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AccountModel.Cards) }
				);

			if (account is null)
				return AccountServiceErrors.GetByNumberNotFound(iban);

			IEnumerable<CardModel> cards = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.AccountId.Equals(account.Id) && x.UserId.Equals(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			account.Cards = cards.ToList();

			AccountResponse response = _mapper.Map<AccountResponse>(account);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByNumberFailed(iban);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, AccountUpdateRequest updateRequest, CancellationToken cancellationToken = default)
	{
		ErrorOr<Updated> response = new();
		try
		{
			AccountModel? dbAccount = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(updateRequest.Id) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AccountModel.Cards) }
				);

			if (dbAccount is null)
				response.Errors.Add(AccountServiceErrors.UpdateAccountNotFound(updateRequest.Id));

			if (updateRequest.Cards is not null)
				foreach (CardUpdateRequest card in updateRequest.Cards)
				{
					CardModel? dbCard = await _repositoryService.CardRepository.GetByConditionAsync(
						expression: x => x.Id.Equals(card.Id) && x.UserId.Equals(userId) && x.AccountId.Equals(updateRequest.Id),
						trackChanges: false,
						cancellationToken: cancellationToken
						);

					if (dbCard is null)
						response.Errors.Add(AccountServiceErrors.UpdateCardNotFound(card.Id));
				}

			if (response.IsError)
				return response;

			if (dbAccount is not null)
				UpdateAccount(dbAccount, updateRequest);

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, updateRequest, ex);
			return AccountServiceErrors.UpdateAccountFailed;
		}
	}

	private static void UpdateAccount(AccountModel account, AccountUpdateRequest updateRequest)
	{
		account.Provider = updateRequest.Provider;

		if (account.Cards.Any() && updateRequest.Cards is not null && updateRequest.Cards.Any())
			foreach (CardModel card in account.Cards)
			{
				card.CardType = updateRequest.Cards.Where(x => x.Id.Equals(card.Id)).Select(x => x.CardType).First();
				card.ValidUntil = updateRequest.Cards.Where(x => x.Id.Equals(card.Id)).Select(x => x.ValidUntil).First();
			}
	}
}
