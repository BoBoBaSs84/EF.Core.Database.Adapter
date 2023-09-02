using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Entities.Finance;
using Domain.Entities.Identity;
using Domain.Errors;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The card service class.
/// </summary>
internal sealed class CardService : ICardService
{
	private readonly ILoggerService<CardService> _logger;
	private readonly IUserService _userService;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the account service class.
	/// </summary>
	/// <param name="logger">The logger service to use.</param>
	/// <param name="userService">The user service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public CardService(ILoggerService<CardService> logger, IUserService userService, IRepositoryService repositoryService, IMapper mapper)
	{
		_logger = logger;
		_userService = userService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> Create(int userId, int accountId, CardCreateRequest createRequest, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
		ErrorOr<Created> response = new();
		try
		{
			Card? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.PAN == createRequest.PAN,
				cancellationToken: cancellationToken
				);

			if (card is not null)
				response.Errors.Add(CardServiceErrors.CreateNumberConflict(createRequest.PAN));

			Account? account = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId),
				cancellationToken: cancellationToken
				);

			if (account is null)
				response.Errors.Add(CardServiceErrors.CreateAccountIdNotFound(accountId));

			if (response.IsError)
				return response;

			User user = await _userService.FindByIdAsync($"{userId}");
			Card newCard = _mapper.Map<Card>(createRequest);

			newCard.User = user;
			newCard.Account = account!;

			await _repositoryService.CardRepository.CreateAsync(newCard, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(int userId, int cardId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{cardId}" };
		try
		{
			Card? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(cardId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (card is null)
				return CardServiceErrors.GetByIdNotFound(cardId);

			await _repositoryService.CardRepository.DeleteAsync(card);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<CardResponse>> Get(int userId, int cardId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{cardId}" };
		try
		{
			Card? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(cardId),
				cancellationToken: cancellationToken
				);

			if (card is null)
				return CardServiceErrors.GetByIdNotFound(cardId);

			CardResponse response = _mapper.Map<CardResponse>(card);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.GetByIdFailed(cardId);
		}
	}

	public async Task<ErrorOr<CardResponse>> Get(int userId, string pan, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{pan}" };
		try
		{
			Card? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.PAN == pan,
				cancellationToken: cancellationToken
				);

			if (card is null)
				return CardServiceErrors.GetByNumberNotFound(pan);

			CardResponse response = _mapper.Map<CardResponse>(card);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.GetByNumberFailed(pan);
		}
	}

	public async Task<ErrorOr<IEnumerable<CardResponse>>> Get(int userId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Card> cards = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				cancellationToken: cancellationToken
				);

			if (!cards.Any())
				return CardServiceErrors.GetAllNotFound;

			IEnumerable<CardResponse> response = _mapper.Map<IEnumerable<CardResponse>>(cards);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, userId, ex);
			return CardServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(int userId, CardUpdateRequest updateRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			Card? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(updateRequest.Id),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (card is null)
				return CardServiceErrors.GetByIdNotFound(updateRequest.Id);

			card.CardTypeId = updateRequest.CardTypeId;
			card.ValidUntil = updateRequest.ValidUntil;

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, userId, ex);
			return CardServiceErrors.UpdateFailed;
		}
	}
}
