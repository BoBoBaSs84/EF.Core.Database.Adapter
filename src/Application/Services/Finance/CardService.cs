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
			string[] parameters = [$"{userId}", $"{accountId}"];
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid id, CancellationToken token = default)
	{
		try
		{
			int result = await _repositoryService.CardRepository
				.DeleteAsync(id, token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? CardServiceErrors.GetByIdNotFound(id)
				: Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<CardResponse>> GetByCardId(Guid id, CancellationToken token = default)
	{
		try
		{
			CardModel? cardEntity = await _repositoryService.CardRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (cardEntity is null)
				return CardServiceErrors.GetByIdNotFound(id);

			CardResponse response = _mapper.Map<CardResponse>(cardEntity);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.GetByCardIdFailed(id);
		}
	}

	public async Task<ErrorOr<IEnumerable<CardResponse>>> GetByUserId(Guid id, CancellationToken token = default)
	{
		try
		{
			IEnumerable<CardModel> cardEntries = await _repositoryService.CardRepository
				.GetManyByConditionAsync(x => x.UserId.Equals(id), token: token)
				.ConfigureAwait(false);

			IEnumerable<CardResponse> response = _mapper.Map<IEnumerable<CardResponse>>(cardEntries);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.GetByUserIdFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid id, CardUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			int result = await _repositoryService.CardRepository
				.UpdateAsync(id, s => s.SetProperty(p => p.CardType, request.CardType).SetProperty(p => p.ValidUntil, request.ValidUntil), token)
				.ConfigureAwait(false);

			return request.Equals(0)
				? CardServiceErrors.GetByIdNotFound(id)
				: Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.UpdateFailed;
		}
	}
}
