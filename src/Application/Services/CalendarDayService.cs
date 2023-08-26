using Application.Contracts.Responses;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Entities.Common;
using Domain.Errors;
using Domain.Extensions;
using Domain.Extensions.QueryExtensions;

using Microsoft.Extensions.Logging;

namespace Application.Services;

internal sealed class CalendarDayService : ICalendarDayService
{
	private readonly IDateTimeService _dateTimeService;
	private readonly ILoggerWrapper<CalendarDayService> _logger;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> logException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="CalendarDayService"/> class.
	/// </summary>
	/// <param name="dateTimeService">The date time service.</param>
	/// <param name="logger">The logger service.</param>
	/// <param name="repositoryService">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public CalendarDayService(IDateTimeService dateTimeService, ILoggerWrapper<CalendarDayService> logger, IRepositoryService repositoryService, IMapper mapper)
	{
		_dateTimeService = dateTimeService;
		_logger = logger;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CalendarDayResponse>> GetByDate(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _repositoryService.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(CalendarDay.DayType) }
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetByDateNotFound(date);

			CalendarDayResponse response = _mapper.Map<CalendarDayResponse>(calendarDay);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return CalendarDayServiceErrors.GetByDateFailed;
		}
	}

	public async Task<ErrorOr<CalendarDayResponse>> GetById(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _repositoryService.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(id),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(CalendarDay.DayType) }
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetByIdNotFound(id);

			CalendarDayResponse response = _mapper.Map<CalendarDayResponse>(calendarDay);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return CalendarDayServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<CalendarDayResponse>>> GetPagedByParameters(CalendarDayParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CalendarDay> calendarDays = await _repositoryService.CalendarDayRepository.GetManyByConditionAsync(
				queryFilter: x => x.FilterByYear(parameters.Year)
					.FilterByMonth(parameters.Month)
					.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
					.FilterByEndOfMonth(parameters.EndOfMonth),
				orderBy: x => x.OrderBy(x => x.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(CalendarDay.DayType) }
				);

			if (!calendarDays.Any())
				return CalendarDayServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await _repositoryService.CalendarDayRepository.GetCountAsync(
				queryFilter: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate).FilterByEndOfMonth(parameters.EndOfMonth),
				cancellationToken: cancellationToken
				);

			IEnumerable<CalendarDayResponse> result = _mapper.Map<IEnumerable<CalendarDayResponse>>(calendarDays);

			return new PagedList<CalendarDayResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return CalendarDayServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<CalendarDayResponse>> GetCurrentDate(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _repositoryService.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(_dateTimeService.Today),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(CalendarDay.DayType) }
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetCurrentDateNotFound;

			CalendarDayResponse response = _mapper.Map<CalendarDayResponse>(calendarDay);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(logException, ex);
			return CalendarDayServiceErrors.GetCurrentDateFailed;
		}
	}
}
