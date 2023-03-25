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
internal sealed class DayTypeService : IDayTypeService
{
	private readonly ILoggerWrapper<DayTypeService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> logException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="DayTypeService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public DayTypeService(ILoggerWrapper<DayTypeService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<DayTypeResponse>> GetById(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = await _unitOfWork.DayTypeRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(id),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (dayType is null)
				return DayTypeServiceErrors.GetByIdNotFound(id);

			DayTypeResponse response = _mapper.Map<DayTypeResponse>(dayType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, id, ex);
			return DayTypeServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<DayTypeResponse>> GetByName(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = await _unitOfWork.DayTypeRepository.GetByConditionAsync(
				expression: x => x.Name.Equals(name),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (dayType is null)
				return DayTypeServiceErrors.GetByNameNotFound(name);

			DayTypeResponse response = _mapper.Map<DayTypeResponse>(dayType);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, name, ex);
			return DayTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IEnumerable<DayTypeResponse>>> GetAll(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<DayType> dayTypes = await _unitOfWork.DayTypeRepository.GetAllAsync(trackChanges, cancellationToken);

			if (!dayTypes.Any())
				return DayTypeServiceErrors.GetPagedByParametersNotFound;

			IEnumerable<DayTypeResponse> response = _mapper.Map<IEnumerable<DayTypeResponse>>(dayTypes);

			return response.ToList();
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return DayTypeServiceErrors.GetPagedByParametersFailed;
		}
	}
}
