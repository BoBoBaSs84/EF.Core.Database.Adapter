using Application.Contracts.Responses;
using Application.Errors.Base;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using AutoMapper;
using Domain.Entities.Enumerator;
using Domain.Errors;
using Domain.Extensions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class DayTypeService : IDayTypeService
{
	private readonly ILogger<DayTypeService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initilizes an instance of <see cref="DayTypeService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public DayTypeService(ILogger<DayTypeService> logger, IUnitOfWork unitOfWork, IMapper mapper)
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
				return ApiError.CreateNotFound("", "");

			DayTypeResponse result = _mapper.Map<DayTypeResponse>(dayType);

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailure("", "");
		}
	}

	public async Task<ErrorOr<DayTypeResponse>> GetByName(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			DayType? dayType = await _unitOfWork.DayTypeRepository.GetByConditionAsync(
				expression: x => x.Name == name,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (dayType is null)
				return ApiError.CreateNotFound("", "");

			DayTypeResponse result = _mapper.Map<DayTypeResponse>(dayType);

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailure("", "");
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

			if (dayTypes is null)
				return ApiError.NotFound("", "");

			IEnumerable<DayTypeResponse> result = _mapper.Map<IEnumerable<DayTypeResponse>>(dayTypes);

			int totalCount = _unitOfWork.DayTypeRepository.QueryCount;

			return new PagedList<DayTypeResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailure("", "");
		}
	}
}
