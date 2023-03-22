using Application.Contracts.Requests;
using Application.Contracts.Responses;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Logging;
using AutoMapper;
using Domain.Entities.Private;
using Domain.Errors;
using Domain.Extensions;
using Domain.Extensions.QueryExtensions;
using Domain.Results;
using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The attendance service class.
/// </summary>
internal sealed class AttendanceService : IAttendanceService
{
	private readonly ILoggerWrapper<AttendanceService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="AttendanceService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AttendanceService(ILoggerWrapper<AttendanceService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	// TODO: Errors!
	public async Task<ErrorOr<Created>> Create(int userId, AttendanceCreateRequest createRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			Attendance newAttendance = _mapper.Map<Attendance>(createRequest);
			newAttendance.UserId = userId;

			await _unitOfWork.AttendanceRepository.CreateAsync(newAttendance, cancellationToken);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<Created>> Create(int userId, IEnumerable<AttendanceCreateRequest> createRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Attendance> newAttendances = _mapper.Map<IEnumerable<Attendance>>(createRequest);

			foreach (Attendance attendance in newAttendances)
				attendance.UserId = userId;

			await _unitOfWork.AttendanceRepository.CreateAsync(newAttendances, cancellationToken);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<Deleted>> Delete(int userId, int calendarDayId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayId}" };
		try
		{
			Attendance? dbAttendance = await _unitOfWork.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarDayId.Equals(calendarDayId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (dbAttendance is null)
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			await _unitOfWork.AttendanceRepository.DeleteAsync(dbAttendance);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<Deleted>> Delete(int userId, IEnumerable<int> calendarDayIds, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayIds.ToJsonString()}" };
		try
		{
			IEnumerable<Attendance> dbAttendances = await _unitOfWork.AttendanceRepository.GetManyByConditionAsync(
				expression: x => calendarDayIds.Contains(x.CalendarDayId) && x.UserId.Equals(userId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (!dbAttendances.Any())
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			await _unitOfWork.AttendanceRepository.DeleteAsync(dbAttendances);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParameters(int userId, AttendanceParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Attendance> attendances = await _unitOfWork.AttendanceRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				filterBy: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate),
				orderBy: x => x.OrderBy(x => x.CalendarDay.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!attendances.Any())
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			int totalCount = _unitOfWork.CalendarDayRepository.QueryCount;

			IEnumerable<AttendanceResponse> result = _mapper.Map<IEnumerable<AttendanceResponse>>(attendances);

			return new PagedList<AttendanceResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<AttendanceResponse>> GetByDate(int userId, DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			Attendance? attendance = await _unitOfWork.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarDay.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (attendance is null)
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			AttendanceResponse result = _mapper.Map<AttendanceResponse>(attendance);

			return result;

		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, new string[] { $"{userId}", $"{date}" }, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<AttendanceResponse>> GetById(int userId, int calendarDayId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayId}" };
		try
		{
			Attendance? attendance = await _unitOfWork.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarDayId.Equals(calendarDayId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (attendance is null)
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			AttendanceResponse result = _mapper.Map<AttendanceResponse>(attendance);

			return result;

		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<Updated>> Update(AttendanceUpdadteRequest updateRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			Attendance attendance = _mapper.Map<Attendance>(updateRequest);

			await _unitOfWork.AttendanceRepository.UpdateAsync(attendance);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, updateRequest, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	// TODO: Errors!
	public async Task<ErrorOr<Updated>> Update(IEnumerable<AttendanceUpdadteRequest> updateRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Attendance> attendances = _mapper.Map<IEnumerable<Attendance>>(updateRequest);

			await _unitOfWork.AttendanceRepository.UpdateAsync(attendances);
			_ = await _unitOfWork.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, updateRequest, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}
}
