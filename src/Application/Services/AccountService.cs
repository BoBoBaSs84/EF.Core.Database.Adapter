using Application.Contracts.Responses.Finance;
using Application.Errors.Base;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Persistence;
using AutoMapper;
using Domain.Entities.Finance;
using Domain.Errors;
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
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="AccountService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AccountService(ILoggerWrapper<AccountService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<IEnumerable<AccountResponse>>> GetAll(int userId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Account> accounts = await _unitOfWork.AccountRepository.GetManyByConditionAsync(
				expression: x => x.AccountUsers.Select(x => x.UserId).Contains(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!accounts.Any())
				return AccountServiceErrors.GetAllNotFound;

			IEnumerable<AccountResponse> result = _mapper.Map<IEnumerable<AccountResponse>>(accounts);

			return result.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, userId, ex);
			return AccountServiceErrors.GetAllFailed;
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
				cancellationToken: cancellationToken
				);

			if (account is null)
				return AccountServiceErrors.GetByNumberNotFound(iban);

			AccountResponse result = _mapper.Map<AccountResponse>(account);

			return result;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AccountServiceErrors.GetByNumberFailed(iban);
		}
	}
}
