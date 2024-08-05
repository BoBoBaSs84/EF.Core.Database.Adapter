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
/// The card service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
/// <param name="mapper">The auto mapper to use.</param>
internal sealed class CardService(ILoggerService<CardService> loggerService, IRepositoryService repositoryService, IMapper mapper) : ICardService
{
	private readonly ILoggerService<CardService> _loggerService = loggerService;
	private readonly IRepositoryService _repositoryService = repositoryService;
	private readonly IMapper _mapper = mapper;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> Create(Guid userId, Guid accountId, CardCreateRequest request, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", $"{accountId}"];
		try
		{
			ErrorOr<Created> response = new();

			if (RegexStatics.CreditCard.IsMatch(request.PAN).Equals(false))
				response.Errors.Add(CardServiceErrors.CreateNumberInvalid(request.PAN));

			AccountModel? accountEntry = await _repositoryService.AccountRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(accountId) && x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: true,
				token: token
				);

			if (accountEntry is null)
				response.Errors.Add(CardServiceErrors.CreateAccountIdNotFound(accountId));

			CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.PAN == request.PAN,
				token: token
				);

			if (cardEntry is not null)
				response.Errors.Add(CardServiceErrors.CreateNumberConflict(request.PAN));

			if (response.IsError)
				return response;

			CardModel newCard = _mapper.Map<CardModel>(request);

			newCard.UserId = userId;
			newCard.Account = accountEntry!;

			await _repositoryService.CardRepository.CreateAsync(newCard, token);
			_ = await _repositoryService.CommitChangesAsync(token);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, Guid cardId, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", $"{cardId}"];
		try
		{
			CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(cardId),
				trackChanges: true,
				token: token
				);

			if (cardEntry is null)
				return CardServiceErrors.GetByIdNotFound(cardId);

			await _repositoryService.CardRepository.DeleteAsync(cardEntry);
			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<CardResponse>> Get(Guid userId, Guid cardId, bool trackChanges = false, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", $"{cardId}"];
		try
		{
			CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(cardId),
				token: token
				);

			if (cardEntry is null)
				return CardServiceErrors.GetByIdNotFound(cardId);

			CardResponse response = _mapper.Map<CardResponse>(cardEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.GetByIdFailed(cardId);
		}
	}

	public async Task<ErrorOr<CardResponse>> Get(Guid userId, string pan, bool trackChanges = false, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", $"{pan}"];
		try
		{
			CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.PAN == pan,
				token: token
				);

			if (cardEntry is null)
				return CardServiceErrors.GetByNumberNotFound(pan);

			CardResponse response = _mapper.Map<CardResponse>(cardEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.GetByNumberFailed(pan);
		}
	}

	public async Task<ErrorOr<IEnumerable<CardResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken token = default)
	{
		try
		{
			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				token: token
				);

			if (cardEntries.Any().Equals(false))
				return CardServiceErrors.GetAllNotFound;

			IEnumerable<CardResponse> response = _mapper.Map<IEnumerable<CardResponse>>(cardEntries);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, userId, ex);
			return CardServiceErrors.GetAllFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, CardUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			CardModel? cardEntry = await _repositoryService.CardRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Id.Equals(request.Id),
				trackChanges: true,
				token: token
				);

			if (cardEntry is null)
				return CardServiceErrors.GetByIdNotFound(request.Id);

			cardEntry.ValidUntil = request.ValidUntil;

			_ = await _repositoryService.CommitChangesAsync(token);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, userId, ex);
			return CardServiceErrors.UpdateFailed;
		}
	}
}
