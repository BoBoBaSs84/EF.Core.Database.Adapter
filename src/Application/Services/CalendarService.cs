using Application.Contracts.Features.Requests;
using Application.Contracts.Features.Responses;
using Application.Contracts.Responses;
using Application.Errors.Base;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using AutoMapper;
using Domain.Entities.Private;
using Domain.Errors;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class CalendarService : ICalendarService
{
	private readonly ILogger<CalendarService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public CalendarService(ILogger<CalendarService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<IPagedList<CalendarResponse>>> GetAllPaged(CalendarParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CalendarDay> calendarDays = await _unitOfWork.CalendarDayRepository.GetManyByConditionAsync(
				expression: null!,
				orderBy: x => x.OrderBy(x => x.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			IEnumerable<CalendarDay> allDays = await _unitOfWork.CalendarDayRepository.GetAllAsync(trackChanges, cancellationToken);

			IEnumerable<CalendarResponse> result = _mapper.Map<IEnumerable<CalendarResponse>>(calendarDays);

			return new PagedList<CalendarResponse>(result.ToList(), allDays.Count(), parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailure("", "");
		}
	}
}
