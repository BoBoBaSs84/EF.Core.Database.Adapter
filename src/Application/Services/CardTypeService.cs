using Application.Contracts.Responses.Enumerators;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;

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
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the card type service class.
	/// </summary>
	/// <param name="logger">The logger service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public CardTypeService(ILoggerService<CardTypeService> logger, IMapper mapper)
	{
		_logger = logger;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CardTypeResponse>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = (CardType)id;

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
			CardType? cardType = (CardType)Enum.Parse(typeof(CardType), name);

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
			IEnumerable<CardType> cardTypes = CardType.CREDIT.GetListFromEnum();

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
