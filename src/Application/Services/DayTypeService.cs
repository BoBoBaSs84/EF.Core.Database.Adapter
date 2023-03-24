using Application.Contracts.Responses.Enumerator;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Logging;
using AutoMapper;
using Domain.Entities.Enumerator;
using Domain.Errors;
using Domain.Extensions.QueryExtensions;
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
			_logger.Log(logException, ex);
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
			_logger.Log(logException, ex);
			return DayTypeServiceErrors.GetByNameFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<DayTypeResponse>>> GetPagedByParameters(DayTypeParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<DayType> dayTypes = await _unitOfWork.DayTypeRepository.GetManyByConditionAsync(
				filterBy: x => x.FilterByIsActive(parameters.IsActive).SearchByName(parameters.Name).SearchByDescription(parameters.Description),
				orderBy: x => x.OrderBy(x => x.Id),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!dayTypes.Any())
				return DayTypeServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await _unitOfWork.DayTypeRepository.GetCountAsync(
				filterBy: x => x.FilterByIsActive(parameters.IsActive).SearchByName(parameters.Name).SearchByDescription(parameters.Description),
				cancellationToken: cancellationToken
				);

			IEnumerable<DayTypeResponse> response = _mapper.Map<IEnumerable<DayTypeResponse>>(dayTypes);

			return new PagedList<DayTypeResponse>(response, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return DayTypeServiceErrors.GetPagedByParametersFailed;
		}
	}
}
