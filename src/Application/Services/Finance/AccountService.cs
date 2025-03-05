using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Application.Services.Finance;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

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
			AccountEntity? accountEntity = await repositoryService.AccountRepository
				.GetByConditionAsync(expression: x => x.IBAN == request.IBAN, token: token)
				.ConfigureAwait(false);

			if (accountEntity is not null)
				return AccountServiceErrors.CreateAccountNumberConflict(request.IBAN);

			if (request.Cards is not null)
			{
				foreach (CardCreateRequest cardRequest in request.Cards)
				{
					CardEntity? cardEntity = await repositoryService.CardRepository
						.GetByConditionAsync(expression: x => x.PAN == cardRequest.PAN, token: token)
						.ConfigureAwait(false);

					if (cardEntity is not null)
						return AccountServiceErrors.CreateCardNumberConflict(cardRequest.PAN);
				}
			}

			AccountEntity account = mapper.Map<AccountEntity>(request);

			if (account.Cards is not null && account.Cards.Count > 0)
			{
				foreach (CardEntity card in account.Cards)
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
			AccountEntity? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (accountEntity is null)
				return AccountServiceErrors.DeleteAccountNotFound(id);

			await repositoryService.AccountRepository
				.DeleteAsync(accountEntity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
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
			AccountEntity? accountEntity = await repositoryService.AccountRepository
				.GetByIdAsync(id, token: token, includeProperties: nameof(AccountEntity.Cards))
				.ConfigureAwait(false);

			if (accountEntity is null)
				return AccountServiceErrors.GetByIdNotFound(id);

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
			IEnumerable<AccountEntity> accountEntities = await repositoryService.AccountRepository
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
			AccountEntity? account = await repositoryService.AccountRepository
				.GetByIdAsync(id, trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (account is null)
				return AccountServiceErrors.UpdateAccountNotFound(id);

			_ = mapper.Map(request, account);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return AccountServiceErrors.UpdateAccountFailed(id);
		}
	}
}
