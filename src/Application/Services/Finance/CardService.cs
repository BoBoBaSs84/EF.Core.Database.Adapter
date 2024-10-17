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
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> Create(Guid userId, Guid accountId, CardCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(accountId, token: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return CardServiceErrors.CreateAccountIdNotFound(accountId);

			CardModel? cardEntity = await repositoryService.CardRepository
				.GetByConditionAsync(x => x.PAN == request.PAN, token: token)
				.ConfigureAwait(false);

			if (cardEntity is not null)
				return CardServiceErrors.CreateNumberConflict(request.PAN);

			CardModel newCard = mapper.Map<CardModel>(request);

			newCard.UserId = userId;
			newCard.AccountId = accountId;

			await repositoryService.CardRepository.CreateAsync(newCard, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{accountId}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed(accountId);
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid id, CancellationToken token = default)
	{
		try
		{
			CardModel? cardEntity = await repositoryService.CardRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (cardEntity is null)
				return CardServiceErrors.DeleteNotFound(id);

			await repositoryService.CardRepository.DeleteAsync(cardEntity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.DeleteFailed(id);
		}
	}

	public async Task<ErrorOr<CardResponse>> GetById(Guid id, CancellationToken token = default)
	{
		try
		{
			CardModel? cardEntity = await repositoryService.CardRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (cardEntity is null)
				return CardServiceErrors.GetByIdNotFound(id);

			CardResponse response = mapper.Map<CardResponse>(cardEntity);

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IEnumerable<CardResponse>>> GetByUserId(Guid id, CancellationToken token = default)
	{
		try
		{
			IEnumerable<CardModel> cardEntries = await repositoryService.CardRepository
				.GetManyByConditionAsync(x => x.UserId.Equals(id), token: token)
				.ConfigureAwait(false);

			IEnumerable<CardResponse> response = mapper.Map<IEnumerable<CardResponse>>(cardEntries);

			return response.ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.GetByUserIdFailed(id);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid id, CardUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			CardModel? cardEntity = await repositoryService.CardRepository
				.GetByIdAsync(id, trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (cardEntity is null)
				return CardServiceErrors.UpdateNotFound(id);

			_ = mapper.Map(request, cardEntity);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.UpdateFailed(id);
		}
	}
}
