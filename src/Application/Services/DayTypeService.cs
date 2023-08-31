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

	public async Task<ErrorOr<DayTypeResponse>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = await _repositoryService.DayTypeRepository
				.GetByConditionAsync(expression: x => x.Id.Equals(id), trackChanges: trackChanges, cancellationToken: cancellationToken);

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
			DayType? dayType = await _repositoryService.DayTypeRepository
				.GetByConditionAsync(expression: x => x.Name.Equals(name), trackChanges: trackChanges, cancellationToken: cancellationToken);

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
			IEnumerable<DayType> dayTypes = await _repositoryService.DayTypeRepository
				.GetAllAsync(ignoreQueryFilters: true, trackChanges: trackChanges, cancellationToken: cancellationToken);

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
