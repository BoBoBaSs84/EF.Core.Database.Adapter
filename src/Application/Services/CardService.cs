using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Entities.Identity;
using Domain.Errors;
using Domain.Models.Finance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The card service class.
/// </summary>
internal sealed class CardService : ICardService
{
	private readonly ILoggerService<CardService> _loggerService;
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
	public CardService(ILoggerService<CardService> loggerService, IUserService userService, IRepositoryService repositoryService, IMapper mapper)
	{
		_loggerService = loggerService;
		_userService = userService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, Guid accountId, CardCreateRequest request, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{accountId}" };
		ErrorOr<Created> response = new();
		try
		{
			CardModel? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.PAN == request.PAN,
				cancellationToken: cancellationToken
				);

			if (card is not null)
				response.Errors.Add(CardServiceErrors.CreateNumberConflict(request.PAN));

			AccountModel? account = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId),
				cancellationToken: cancellationToken
				);

			if (account is null)
				response.Errors.Add(CardServiceErrors.CreateAccountIdNotFound(accountId));

			if (response.IsError)
				return response;

			UserModel user = await _userService.FindByIdAsync($"{userId}");
			CardModel newCard = _mapper.Map<CardModel>(request);

			newCard.User = user;
			newCard.Account = account!;

			await _repositoryService.CardRepository.CreateAsync(newCard, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, Guid cardId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{cardId}" };
		try
		{
			CardModel? card = await _repositoryService.CardRepository.GetByConditionAsync(
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
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<CardResponse>> Get(Guid userId, Guid cardId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{cardId}" };
		try
		{
			CardModel? card = await _repositoryService.CardRepository.GetByConditionAsync(
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
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.GetByIdFailed(cardId);
		}
	}

	public async Task<ErrorOr<CardResponse>> Get(Guid userId, string pan, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{pan}" };
		try
		{
			CardModel? card = await _repositoryService.CardRepository.GetByConditionAsync(
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
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.GetByNumberFailed(pan);
		}
	}

	public async Task<ErrorOr<IEnumerable<CardResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CardModel> cards = await _repositoryService.CardRepository.GetManyByConditionAsync(
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
			_loggerService.Log(LogExceptionWithParams, userId, ex);
			return CardServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, CardUpdateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			CardModel? card = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(request.Id),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (card is null)
				return CardServiceErrors.GetByIdNotFound(request.Id);

			card.CardType = request.CardType;
			card.ValidUntil = request.ValidUntil;

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, userId, ex);
			return CardServiceErrors.UpdateFailed;
		}
	}
}
