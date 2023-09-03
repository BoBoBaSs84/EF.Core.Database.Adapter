using Application.Contracts.Responses.Common;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Extensions;
using Domain.Extensions.QueryExtensions;
using Domain.Models.Common;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The calendar day service class.
/// </summary>
internal sealed class CalendarDayService : ICalendarDayService
{
	private readonly IDateTimeService _dateTimeService;
	private readonly ILoggerService<CalendarDayService> _logger;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the calendar day service class.
	/// </summary>
	/// <param name="dateTimeService">The date time service to use.</param>
	/// <param name="logger">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public CalendarDayService(IDateTimeService dateTimeService, ILoggerService<CalendarDayService> logger, IRepositoryService repositoryService, IMapper mapper)
	{
		_dateTimeService = dateTimeService;
		_logger = logger;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CalendarDayResponse>> Get(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _repositoryService.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetByDateNotFound(date);

			CalendarDayResponse response = _mapper.Map<CalendarDayResponse>(calendarDay);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return CalendarDayServiceErrors.GetByDateFailed;
		}
	}

	public async Task<ErrorOr<CalendarDayResponse>> Get(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _repositoryService.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(id),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetByIdNotFound(id);

			CalendarDayResponse response = _mapper.Map<CalendarDayResponse>(calendarDay);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return CalendarDayServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<CalendarDayResponse>>> Get(CalendarDayParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
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
				cancellationToken: cancellationToken
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
			_logger.Log(LogExceptionWithParams, parameters, ex);
			return CalendarDayServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<CalendarDayResponse>> Get(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _repositoryService.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(_dateTimeService.Today),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetCurrentDateNotFound;

			CalendarDayResponse response = _mapper.Map<CalendarDayResponse>(calendarDay);

			return response;
		}
		catch (Exception ex)
		{
			_logger.Log(LogException, ex);
			return CalendarDayServiceErrors.GetCurrentDateFailed;
		}
	}
}
