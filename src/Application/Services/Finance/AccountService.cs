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
/// The account service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
/// <param name="mapper">The auto mapper to use.</param>
internal sealed class AccountService(ILoggerService<AccountService> loggerService, IRepositoryService repositoryService, IMapper mapper) : IAccountService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> Create(Guid id, AccountCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntity = await repositoryService.AccountRepository
				.GetByConditionAsync(expression: x => x.IBAN == request.IBAN, token: token)
				.ConfigureAwait(false);

			if (accountEntity is not null)
				return AccountServiceErrors.CreateAccountNumberConflict(request.IBAN);

			if (request.Cards is not null)
			{
				foreach (CardCreateRequest cardRequest in request.Cards)
				{
					CardModel? cardEntity = await repositoryService.CardRepository
						.GetByConditionAsync(expression: x => x.PAN == cardRequest.PAN, token: token)
						.ConfigureAwait(false);

					if (cardEntity is not null)
						return AccountServiceErrors.CreateCardNumberConflict(cardRequest.PAN);
				}
			}

			AccountModel account = mapper.Map<AccountModel>(request);

			if (account.Cards is not null && account.Cards.Count > 0)
			{
				foreach (CardModel card in account.Cards)
					card.UserId = id;
			}

			account.AccountUsers = [new() { UserId = id, Account = account }];

			await repositoryService.AccountRepository.CreateAsync(account, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return AccountServiceErrors.CreateAccountFailed(request.IBAN);
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid id, CancellationToken token = default)
	{
		try
		{
			int result = await repositoryService.AccountRepository
				.DeleteAsync(id, token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? AccountServiceErrors.DeleteAccountNotFound(id)
				: Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return AccountServiceErrors.DeleteAccountFailed(id);
		}
	}

	public async Task<ErrorOr<AccountResponse>> GetById(Guid id, CancellationToken token = default)
	{
		try
		{
			AccountModel? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return AccountServiceErrors.GetByIdNotFound(id);

			IEnumerable<CardModel> cardEntities = await repositoryService.CardRepository
				.GetManyByConditionAsync(x => x.UserId.Equals(id) && x.AccountId.Equals(id), token: token)
				.ConfigureAwait(false);

			if (cardEntities.Any())
				accountEntity.Cards = cardEntities.ToList();

			AccountResponse response = mapper.Map<AccountResponse>(accountEntity);

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return AccountServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IEnumerable<AccountResponse>>> GetByUserId(Guid id, CancellationToken token = default)
	{
		try
		{
			IEnumerable<AccountModel> accountEntities = await repositoryService.AccountRepository
				.GetManyByConditionAsync(x => x.AccountUsers.Select(x => x.UserId).Contains(id), token: token)
				.ConfigureAwait(false);

			IEnumerable<AccountResponse> result = mapper.Map<IEnumerable<AccountResponse>>(accountEntities);

			return result.ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return AccountServiceErrors.GetByUserIdFailed(id);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid id, AccountUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			int result = await repositoryService.AccountRepository
				.UpdateAsync(id, s => s.SetProperty(p => p.Type, request.Type).SetProperty(p => p.Provider, request.Provider), token)
				.ConfigureAwait(false);

			return result.Equals(0)
				? AccountServiceErrors.UpdateAccountNotFound(id)
				: Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return AccountServiceErrors.UpdateAccountFailed(id);
		}
	}
}
