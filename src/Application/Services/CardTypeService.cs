using Application.Contracts.Responses.Enumerator;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Entities.Enumerator;
using Domain.Errors;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The card type service class.
/// </summary>
[SuppressMessage("Globalization", "CA1309", Justification = "Overload with 'StringComparison' parameter is not supported.")]
internal sealed class CardTypeService : ICardTypeService
{
	private readonly ILoggerService<CardTypeService> _logger;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the card type service class.
	/// </summary>
	/// <param name="logger">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	internal CardTypeService(ILoggerService<CardTypeService> logger, IRepositoryService repositoryService, IMapper mapper)
	{
		_logger = logger;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CardTypeResponse>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = await _repositoryService.CardTypeRepository
				.GetByConditionAsync(expression: x => x.Id.Equals(id), trackChanges: trackChanges, cancellationToken: cancellationToken);

			if (cardType is null)
				return CardTypeServiceErrors.GetByIdNotFound(id);

			CardTypeResponse response = _mapper.Map<CardTypeResponse>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, id, ex);
			return CardTypeServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<CardTypeResponse>> Get(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = await _repositoryService.CardTypeRepository
				.GetByConditionAsync(expression: x => x.Name.Equals(name), trackChanges: trackChanges, cancellationToken: cancellationToken);

			if (cardType is null)
				return CardTypeServiceErrors.GetByNameNotFound(name);

			CardTypeResponse response = _mapper.Map<CardTypeResponse>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, name, ex);
			return CardTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<CardTypeResponse>>> Get(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CardType> cardTypes = await _repositoryService.CardTypeRepository
				.GetAllAsync(ignoreQueryFilters: true, trackChanges: trackChanges, cancellationToken: cancellationToken);

			if (!cardTypes.Any())
				return CardTypeServiceErrors.GetAllNotFound;

			IEnumerable<CardTypeResponse> response = _mapper.Map<IEnumerable<CardTypeResponse>>(cardTypes);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return CardTypeServiceErrors.GetAllFailed;
		}
	}
}
