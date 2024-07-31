using Application.Contracts.Responses.Common;
using Application.Errors.Services;
using Application.Extensions;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Common;
using Application.Interfaces.Infrastructure.Logging;

using AutoMapper;

using Domain.Errors;

using Microsoft.Extensions.Logging;

using DR = Application.Common.Statics.DateRanges;

namespace Application.Services.Common;

/// <summary>
/// The implementation of the calendar service.
/// </summary>
/// <param name="dateTimeService">The date time service instance to use.</param>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class CalendarDayService(IDateTimeService dateTimeService, ILoggerService<CalendarDayService> loggerService, IMapper mapper) : ICalendarDayService
{
	private readonly IQueryable<DateTime> _dateTimes = GetPossibleDates();

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public ErrorOr<CalendarResponse> GetByDate(DateTime date)
	{
		try
		{
			DateTime? calendarDay = _dateTimes.FirstOrDefault(x => x.Date.Equals(date.Date));

			if (calendarDay is null)
				return CalendarServiceErrors.GetByDateNotFound(date);

			CalendarResponse respone = mapper.Map<CalendarResponse>(calendarDay);

			return respone;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, date, ex);
			return CalendarServiceErrors.GetByDateFailed;
		}
	}

	public ErrorOr<IPagedList<CalendarResponse>> GetPagedByParameters(CalendarParameters parameters)
	{
		try
		{
			IEnumerable<DateTime> calendarDays = [.. _dateTimes.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
				.OrderBy(o => o)
				.Take(parameters.PageSize)
				.Skip((parameters.PageNumber - 1) * parameters.PageSize)];

			int totalCount = _dateTimes.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
				.Count();

			IEnumerable<CalendarResponse> result = mapper.Map<IEnumerable<CalendarResponse>>(calendarDays);

			return new PagedList<CalendarResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CalendarServiceErrors.GetPagedByParametersFailed;
		}
	}

	public ErrorOr<CalendarResponse> GetCurrent()
	{
		try
		{
			DateTime? calendarDay = _dateTimes.FirstOrDefault(x => x.Date.Equals(dateTimeService.Today));

			if (calendarDay is null)
				return CalendarServiceErrors.GetByDateNotFound(dateTimeService.Today);

			CalendarResponse respone = mapper.Map<CalendarResponse>(calendarDay);

			return respone;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return CalendarServiceErrors.GetByDateFailed;
		}
	}

	private static IQueryable<DateTime> GetPossibleDates()
	{
		return Enumerable.Range(0, (DR.MaxDate - DR.MinDate).Days)
			.Select(i => DR.MinDate.AddDays(i))
			.AsQueryable();
	}
}
