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
/// The day type service class.
/// </summary>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class DayTypeService : IDayTypeService
{
	private readonly ILoggerService<DayTypeService> _logger;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the day type service class.
	/// </summary>
	/// <param name="logger">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public DayTypeService(ILoggerService<DayTypeService> logger, IRepositoryService repositoryService, IMapper mapper)
	{
		_logger = logger;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<DayType>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = (DayType)id;

			if (dayType is null)
				return DayTypeServiceErrors.GetByIdNotFound(id);

			DayType response = _mapper.Map<DayType>(dayType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, id, ex);
			return DayTypeServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<DayType>> Get(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = (DayType)Enum.Parse(typeof(DayType), name);

			if (dayType is null)
				return DayTypeServiceErrors.GetByNameNotFound(name);

			DayType response = _mapper.Map<DayType>(dayType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogExceptionWithParams, name, ex);
			return DayTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<DayType>>> Get(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<DayType> dayTypes = DayType.HOLIDAY.GetListFromEnum();

			if (!dayTypes.Any())
				return DayTypeServiceErrors.GetAllNotFound;

			IEnumerable<DayType> response = _mapper.Map<IEnumerable<DayType>>(dayTypes);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return DayTypeServiceErrors.GetAllFailed;
		}
	}
}
