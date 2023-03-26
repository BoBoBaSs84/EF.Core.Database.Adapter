using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Services;
using AutoMapper;
using Domain.Entities.Enumerator;
using Domain.Entities.Finance;
using Domain.Entities.Identity;
using Domain.Errors;
using Domain.Results;
using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The account service class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IAccountService"/> interface.
/// </remarks>
internal sealed class AccountService : IAccountService
{
	private readonly ILoggerWrapper<AccountService> _logger;
	private readonly IUserService _userService;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="AccountService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="userService">The user service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AccountService(ILoggerWrapper<AccountService> logger, IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_userService = userService;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> Create(int userId, AccountCreateRequest createRequest, CancellationToken cancellationToken = default)
	{
		ErrorOr<Created> response = new();
		try
		{
			Account? dbAccount = await _unitOfWork.AccountRepository.GetByConditionAsync(x => x.IBAN == createRequest.IBAN, false, cancellationToken);

			if (dbAccount is not null)
				response.Errors.Add(AccountServiceErrors.CreateAccountNumberConflict(dbAccount.IBAN));

			if (createRequest.Cards is not null)
				foreach (CardCreateRequest card in createRequest.Cards)
				{
					Card? dbCard = await _unitOfWork.CardRepository.GetByConditionAsync(x => x.PAN == card.PAN, false, cancellationToken);

					if (dbCard is not null)
						response.Errors.Add(AccountServiceErrors.CreateCardNumberConflict(dbCard.PAN));
				}

			if (response.IsError)
				return response;

			User dbUser = await _userService.FindByIdAsync($"{userId}");
			Account newAccount = _mapper.Map<Account>(createRequest);

			if (newAccount.Cards is not null)
				foreach (Card card in newAccount.Cards)
					card.User = dbUser;

			AccountUser newAccountUser = new() { User = dbUser, Account = newAccount };
			newAccount.AccountUsers.Add(newAccountUser);

			await _unitOfWork.AccountRepository.CreateAsync(newAccount, cancellationToken);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AccountServiceErrors.CreateAccountFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(int userId, int accountId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
		try
		{
			Account? dbAccount = await _unitOfWork.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (dbAccount is null)
				return AccountServiceErrors.DeleteAccountNotFound(accountId);

			await _unitOfWork.AccountRepository.DeleteAsync(dbAccount);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AccountServiceErrors.DeleteAccountFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<AccountResponse>>> GetAll(int userId, bool trackChanges = false, CancellationToken cancellationToken = default)
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
			IEnumerable<Account> accounts = await _unitOfWork.AccountRepository.GetManyByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!accounts.Any())
				return AccountServiceErrors.GetAllNotFound;

			IEnumerable<Card> cards = await _unitOfWork.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(Card.CardType) }
				);

			foreach (Account account in accounts)
				account.Cards = cards.Where(x => x.AccountId.Equals(account.Id)).ToList();

			IEnumerable<AccountResponse> result = _mapper.Map<IEnumerable<AccountResponse>>(accounts);

			return result.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, userId, ex);
			return AccountServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<AccountResponse>> GetById(int userId, int accountId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
		try
		{
			Account? account = await _unitOfWork.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(Account.Cards) }
				);

			if (account is null)
				return AccountServiceErrors.GetByIdNotFound(accountId);

			AccountResponse response = _mapper.Map<AccountResponse>(account);

			return response;
		}
		catch(Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByIdFailed(accountId);
		}
	}

	public async Task<ErrorOr<AccountResponse>> GetByNumber(int userId, string iban, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{iban}" };
		try
		{
			Account? account = await _unitOfWork.AccountRepository.GetByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId) && x.IBAN == iban,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(Account.Cards) }
				);

			if (account is null)
				return AccountServiceErrors.GetByNumberNotFound(iban);

			IEnumerable<Card> cards = await _unitOfWork.CardRepository.GetManyByConditionAsync(
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
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByNumberFailed(iban);
		}
	}

	public async Task<ErrorOr<Updated>> Update(int userId, AccountUpdateRequest updateRequest, CancellationToken cancellationToken = default)
	{
		ErrorOr<Updated> response = new();
		try
		{
			Account? dbAccount = await _unitOfWork.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(updateRequest.Id) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(Account.Cards) }
				);

			if (dbAccount is null)
				response.Errors.Add(AccountServiceErrors.UpdateAccountNotFound(updateRequest.Id));

			if (updateRequest.Cards is not null)
				foreach (CardUpdateRequest card in updateRequest.Cards)
				{
					Card? dbCard = await _unitOfWork.CardRepository.GetByConditionAsync(
						expression: x => x.Id.Equals(card.Id) && x.UserId.Equals(userId) && x.AccountId.Equals(updateRequest.Id),
						trackChanges: false,
						cancellationToken: cancellationToken
						);

					if (dbCard is null)
						response.Errors.Add(AccountServiceErrors.UpdateCardNotFound(card.Id));

					CardType? dbCardType = await _unitOfWork.CardTypeRepository
						.GetByConditionAsync(x => x.Id.Equals(card.CardTypeId), false, cancellationToken);

					if (dbCardType is null)
						response.Errors.Add(AccountServiceErrors.UpdateCardTypeNotFound(card.CardTypeId));
				}

			if (response.IsError)
				return response;

			if (dbAccount is not null)
				UpdateAccount(dbAccount, updateRequest);

			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, updateRequest, ex);
			return AccountServiceErrors.UpdateAccountFailed;
		}
	}

	private static void UpdateAccount(Account account, AccountUpdateRequest updateRequest)
	{
		account.Provider = updateRequest.Provider;

		if (account.Cards.Any() && updateRequest.Cards is not null && updateRequest.Cards.Any())
			foreach (Card card in account.Cards)
			{
				card.CardTypeId = updateRequest.Cards.Where(x => x.Id.Equals(card.Id)).Select(x => x.CardTypeId).First();
				card.ValidUntil = updateRequest.Cards.Where(x => x.Id.Equals(card.Id)).Select(x => x.ValidUntil).First();
			}
	}
}
