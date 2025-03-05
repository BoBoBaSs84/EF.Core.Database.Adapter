using Application.Contracts.Responses.Common;
using Application.Errors.Services;
using Application.Extensions;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Providers;
using Application.Interfaces.Application.Services.Common;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Home.Domain.Errors;

using Microsoft.Extensions.Logging;

using DateStatics = Application.Common.ApplicationStatics.DateStatics;

namespace Application.Services.Common;

/// <summary>
/// The implementation of the calendar service.
/// </summary>
/// <param name="dateTimeService">The date time service instance to use.</param>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class CalendarService(IDateTimeProvider dateTimeService, ILoggerService<CalendarService> loggerService, IMapper mapper) : ICalendarService
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
			DateTime? calendarDay = _dateTimes.Single(x => x.Date.Equals(date.Date));

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
			IEnumerable<DateTime> calendarDays = _dateTimes.FilterByParameters(parameters)
				.OrderBy(o => o)
				.Skip((parameters.PageNumber - 1) * parameters.PageSize)
				.Take(parameters.PageSize);

			int totalCount = _dateTimes.FilterByParameters(parameters)
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
			DateTime? calendarDay = _dateTimes.Single(x => x.Date.Equals(dateTimeService.Today));

			CalendarResponse respone = mapper.Map<CalendarResponse>(calendarDay);

			return respone;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return CalendarServiceErrors.GetCurrentDateFailed;
		}
	}

	private static IQueryable<DateTime> GetPossibleDates()
	{
		IQueryable<DateTime> dates = Enumerable.Range(0, (DateStatics.MaxDate - DateStatics.MinDate).Days)
			.Select(i => DateStatics.MinDate.AddDays(i))
			.AsQueryable();

		return dates;
	}
}
