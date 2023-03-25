using Application.Contracts.Responses;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Persistence;
using AutoMapper;
using Domain.Entities.Private;
using Domain.Errors;
using Domain.Extensions;
using Domain.Extensions.QueryExtensions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal sealed class CalendarDayService : ICalendarDayService
{
	private readonly ILoggerWrapper<CalendarDayService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> logException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="CalendarDayService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public CalendarDayService(ILoggerWrapper<CalendarDayService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CalendarDayResponse>> GetByDate(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarDay? calendarDay = await _unitOfWork.CalendarDayRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetByDateNotFound(date);

			CalendarDayResponse result = _mapper.Map<CalendarDayResponse>(calendarDay);

			return result;
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
			CalendarDay? calendarDay = await _unitOfWork.CalendarDayRepository.GetByIdAsync(id, cancellationToken);

			if (calendarDay is null)
				return CalendarDayServiceErrors.GetByIdNotFound(id);

			CalendarDayResponse result = _mapper.Map<CalendarDayResponse>(calendarDay);

			return result;
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
			IEnumerable<CalendarDay> calendarDays = await _unitOfWork.CalendarDayRepository.GetManyByConditionAsync(
				filterBy: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate),
				orderBy: x => x.OrderBy(x => x.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!calendarDays.Any())
				return CalendarDayServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await _unitOfWork.CalendarDayRepository.GetCountAsync(
				filterBy: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate),
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
}
