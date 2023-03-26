using Application.Contracts.Responses.Enumerator;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Persistence;
using AutoMapper;
using Domain.Entities.Enumerator;
using Domain.Errors;
using Microsoft.Extensions.Logging;

namespace Application.Services;

[SuppressMessage("Globalization", "CA1309",
	Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class CardTypeService : ICardTypeService
{
	private readonly ILoggerWrapper<CardTypeService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> logException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="CardTypeService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public CardTypeService(ILoggerWrapper<CardTypeService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CardTypeResponse>> GetById(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = await _unitOfWork.CardTypeRepository.GetByIdAsync(id, cancellationToken);

			if (cardType is null)
				return CardTypeServiceErrors.GetByIdNotFound(id);

			CardTypeResponse response = _mapper.Map<CardTypeResponse>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, id, ex);
			return CardTypeServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<CardTypeResponse>> GetByName(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = await _unitOfWork.CardTypeRepository.GetByConditionAsync(
				expression: x => x.Name.Equals(name),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (cardType is null)
				return CardTypeServiceErrors.GetByNameNotFound(name);

			CardTypeResponse response = _mapper.Map<CardTypeResponse>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, name, ex);
			return CardTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<CardTypeResponse>>> GetAll(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CardType> cardTypes = await _unitOfWork.CardTypeRepository.GetAllAsync(trackChanges, cancellationToken);

			if (!cardTypes.Any())
				return CardTypeServiceErrors.GetAllNotFound;

			IEnumerable<CardTypeResponse> response = _mapper.Map<IEnumerable<CardTypeResponse>>(cardTypes);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return CardTypeServiceErrors.GetAllFailed;
		}
	}
}
