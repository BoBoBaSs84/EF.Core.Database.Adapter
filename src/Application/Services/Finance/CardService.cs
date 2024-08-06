using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

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
			AccountModel? accountEntity = await _repositoryService.AccountRepository
				.GetByIdAsync(accountId, token: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return CardServiceErrors.CreateAccountIdNotFound(accountId);

			CardModel? cardEntity = await _repositoryService.CardRepository
				.GetByConditionAsync(x => x.PAN == request.PAN, token: token)
				.ConfigureAwait(false);

			if (cardEntity is not null)
				return CardServiceErrors.CreateNumberConflict(request.PAN);

			CardModel newCard = _mapper.Map<CardModel>(request);

			newCard.UserId = userId;
			newCard.AccountId = accountId;

			await _repositoryService.CardRepository.CreateAsync(newCard, token)
				.ConfigureAwait(false);

			_ = await _repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{accountId}"];
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed(accountId);
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
				? CardServiceErrors.DeleteNotFound(id)
				: Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.DeleteFailed(id);
		}
	}

	public async Task<ErrorOr<CardResponse>> GetById(Guid id, CancellationToken token = default)
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
			return CardServiceErrors.GetByIdFailed(id);
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
			return CardServiceErrors.GetByUserIdFailed(id);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid id, CardUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			int result = await _repositoryService.CardRepository
				.UpdateAsync(id, s => s.SetProperty(p => p.Type, request.Type).SetProperty(p => p.ValidUntil, request.ValidUntil), token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? CardServiceErrors.UpdateNotFound(id)
				: Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.UpdateFailed(id);
		}
	}
}
