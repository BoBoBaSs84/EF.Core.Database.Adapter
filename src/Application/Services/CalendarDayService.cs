using Application.Contracts.Responses;
using Application.Errors.Base;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using AutoMapper;
using Domain.Entities.Private;
using Domain.Errors;
using Domain.Extensions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class CalendarDayService : ICalendarDayService
{
	private readonly ILogger<CalendarDayService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initilizes an instance of <see cref="CalendarDayService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public CalendarDayService(ILogger<CalendarDayService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<IPagedList<CalendarDayResponse>>> GetPagedByParameters(CalendarDayParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CalendarDay> calendarDays = await _unitOfWork.CalendarDayRepository.GetManyByConditionAsync(
				filterBy: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate),
				orderBy: x => x.OrderBy(x => x.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			IEnumerable<CalendarDayResponse> result = _mapper.Map<IEnumerable<CalendarDayResponse>>(calendarDays);

			int totalCount = _unitOfWork.CalendarDayRepository.QueryCount;

			return new PagedList<CalendarDayResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailure("", "");
		}
	}
}
