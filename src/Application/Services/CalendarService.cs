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
using Domain.Models.Common;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The calendar service class.
/// </summary>
internal sealed class CalendarService : ICalendarService
{
	private readonly IDateTimeService _dateTimeService;
	private readonly ILoggerService<CalendarService> _loggerService;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the calendar service class.
	/// </summary>
	/// <param name="dateTimeService">The date time service to use.</param>
	/// <param name="loggerService">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public CalendarService(IDateTimeService dateTimeService, ILoggerService<CalendarService> loggerService, IRepositoryService repositoryService, IMapper mapper)
	{
		_dateTimeService = dateTimeService;
		_loggerService = loggerService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CalendarResponse>> Get(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarModel? calendarEntry = await _repositoryService.CalendarRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarEntry is null)
				return CalendarServiceErrors.GetByDateNotFound(date);

			CalendarResponse response = _mapper.Map<CalendarResponse>(calendarEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return CalendarServiceErrors.GetByDateFailed;
		}
	}

	public async Task<ErrorOr<CalendarResponse>> Get(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarModel? calendarEntry = await _repositoryService.CalendarRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(id),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarEntry is null)
				return CalendarServiceErrors.GetByIdNotFound(id);

			CalendarResponse response = _mapper.Map<CalendarResponse>(calendarEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return CalendarServiceErrors.GetByIdFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<CalendarResponse>>> Get(CalendarParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CalendarModel> calendarEntries = await _repositoryService.CalendarRepository.GetManyByConditionAsync(
				queryFilter: x => x.FilterByYear(parameters.Year)
					.FilterByMonth(parameters.Month)
					.FilterByDateRange(parameters.MinDate, parameters.MaxDate),
				orderBy: x => x.OrderBy(x => x.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!calendarEntries.Any())
				return CalendarServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await _repositoryService.CalendarRepository.GetCountAsync(
				queryFilter: x => x.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate),
				cancellationToken: cancellationToken
				);

			IEnumerable<CalendarResponse> result = _mapper.Map<IEnumerable<CalendarResponse>>(calendarEntries);

			return new PagedList<CalendarResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return CalendarServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<CalendarResponse>> Get(bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarModel? calendarEntry = await _repositoryService.CalendarRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(_dateTimeService.Today),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarEntry is null)
				return CalendarServiceErrors.GetCurrentDateNotFound;

			CalendarResponse response = _mapper.Map<CalendarResponse>(calendarEntry);

			return response;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return CalendarServiceErrors.GetCurrentDateFailed;
		}
	}
}
