using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The card type service class.
/// </summary>
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
	public CardTypeService(ILoggerService<CardTypeService> logger, IRepositoryService repositoryService, IMapper mapper)
	{
		_logger = logger;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CardType>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = (CardType)id;

			if (cardType is null)
				return CardTypeServiceErrors.GetByIdNotFound(id);

			CardType response = _mapper.Map<CardType>(cardType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, id, ex);
			return CardTypeServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<CardType>> Get(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = (CardType)Enum.Parse(typeof(CardType), name);

			if (cardType is null)
				return CardTypeServiceErrors.GetByNameNotFound(name);

			CardType response = _mapper.Map<CardType>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, name, ex);
			return CardTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<CardType>>> Get(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CardType> cardTypes = CardType.CREDIT.GetListFromEnum();

			if (!cardTypes.Any())
				return CardTypeServiceErrors.GetAllNotFound;

			IEnumerable<CardType> response = _mapper.Map<IEnumerable<CardType>>(cardTypes);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return CardTypeServiceErrors.GetAllFailed;
		}
	}
}
