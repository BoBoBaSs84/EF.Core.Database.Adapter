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
/// The day type service class.
/// </summary>
internal sealed class DayTypeService : IDayTypeService
{
	private readonly ILoggerService<DayTypeService> _logger;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the day type service class.
	/// </summary>
	/// <param name="logger">The logger service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public DayTypeService(ILoggerService<DayTypeService> logger, IMapper mapper)
	{
		_logger = logger;
		_mapper = mapper;
	}

	public async Task<ErrorOr<DayTypeResponse>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = (DayType)id;

			if (dayType is null)
				return DayTypeServiceErrors.GetByIdNotFound(id);

			DayTypeResponse response = _mapper.Map<DayTypeResponse>(dayType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, id, ex);
			return DayTypeServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<DayTypeResponse>> Get(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = (DayType)Enum.Parse(typeof(DayType), name);

			if (dayType is null)
				return DayTypeServiceErrors.GetByNameNotFound(name);

			DayTypeResponse response = _mapper.Map<DayTypeResponse>(dayType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, name, ex);
			return DayTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<DayTypeResponse>>> Get(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<DayType> dayTypes = DayType.HOLIDAY.GetListFromEnum();

			if (!dayTypes.Any())
				return DayTypeServiceErrors.GetAllNotFound;

			IEnumerable<DayTypeResponse> response = _mapper.Map<IEnumerable<DayTypeResponse>>(dayTypes);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return DayTypeServiceErrors.GetAllFailed;
		}
	}
}
