using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Interfaces.Application.Services.Finance;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Finance;

/// <summary>
/// The card service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
/// <param name="userService">The service providing information about the current user.</param>
internal sealed class CardService(ILoggerService<CardService> loggerService, ICurrentUserService userService, IRepositoryService repositoryService) : ICardService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateAsync(Guid accountId, CardCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountEntity? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(accountId, token: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return CardServiceErrors.CreateAccountIdNotFound(accountId);

			CardEntity? cardEntity = await repositoryService.CardRepository
				.GetByConditionAsync(x => x.PAN == request.PAN, token: token)
				.ConfigureAwait(false);

			if (cardEntity is not null)
				return CardServiceErrors.CreateNumberConflict(request.PAN);

			CardEntity newCard = request.ToEntity(userService.UserId, accountId);

			await repositoryService.CardRepository
				.CreateAsync(newCard, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userService.UserId}", $"{accountId}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CardServiceErrors.CreateFailed(accountId);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default)
	{
		try
		{
			CardEntity? entity = await repositoryService.CardRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return CardServiceErrors.DeleteNotFound(id);

			await repositoryService.CardRepository
				.DeleteAsync(entity, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.DeleteFailed(id);
		}
	}

	public async Task<ErrorOr<CardResponse>> GetByIdAsync(Guid id, CancellationToken token = default)
	{
		try
		{
			CardEntity? entity = await repositoryService.CardRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return CardServiceErrors.GetByIdNotFound(id);

			CardResponse response = entity.ToResponse();

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return CardServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IEnumerable<CardResponse>>> GetAllAsync(CancellationToken token = default)
	{
		try
		{
			IReadOnlyList<CardEntity> entities = await repositoryService.CardRepository
				.GetAllAsync(token: token)
				.ConfigureAwait(false);

			IEnumerable<CardResponse> response = entities.Select(x => x.ToResponse());

			return response.ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, userService.UserId, ex);
			return CardServiceErrors.GetByUserIdFailed(userService.UserId);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateAsync(Guid id, CardUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			CardEntity? entity = await repositoryService.CardRepository
				.GetByIdAsync(id, trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return CardServiceErrors.UpdateNotFound(id);

			entity = request.ToEntity(entity);

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
