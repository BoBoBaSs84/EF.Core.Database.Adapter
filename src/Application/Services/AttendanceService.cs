using Application.Contracts.Requests;
using Application.Contracts.Responses;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;
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
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> logExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of <see cref="AttendanceService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="repositoryService">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public AttendanceService(ILoggerWrapper<AttendanceService> logger, IRepositoryService repositoryService, IMapper mapper)
	{
		_logger = logger;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> Create(int userId, AttendanceCreateRequest createRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			Attendance newAttendance = _mapper.Map<Attendance>(createRequest);
			newAttendance.UserId = userId;

			await _repositoryService.AttendanceRepository.CreateAsync(newAttendance, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AttendanceServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Created>> CreateMany(int userId, IEnumerable<AttendanceCreateRequest> createRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Attendance> newAttendances = _mapper.Map<IEnumerable<Attendance>>(createRequest);

			foreach (Attendance attendance in newAttendances)
				attendance.UserId = userId;

			await _repositoryService.AttendanceRepository.CreateAsync(newAttendances, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, createRequest, ex);
			return AttendanceServiceErrors.CreateManyFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(int userId, int calendarDayId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayId}" };
		try
		{
			Attendance? dbAttendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarDayId.Equals(calendarDayId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (dbAttendance is null)
				return AttendanceServiceErrors.DeleteNotFound;

			await _repositoryService.AttendanceRepository.DeleteAsync(dbAttendance);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteMany(int userId, IEnumerable<int> calendarDayIds, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayIds.ToJsonString()}" };
		try
		{
			IEnumerable<Attendance> dbAttendances = await _repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => calendarDayIds.Contains(x.CalendarDayId) && x.UserId.Equals(userId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (!dbAttendances.Any())
				return AttendanceServiceErrors.DeleteManyNotFound;

			await _repositoryService.AttendanceRepository.DeleteAsync(dbAttendances);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteManyFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParameters(int userId, AttendanceParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Attendance> attendances = await _repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				queryFilter: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate).FilterByEndOfMonth(parameters.EndOfMonth),
				orderBy: x => x.OrderBy(x => x.CalendarDay.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { $"{nameof(Attendance.CalendarDay)}", $"{nameof(Attendance.DayType)}" }
				);

			if (!attendances.Any())
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await _repositoryService.AttendanceRepository.GetCountAsync(
				expression: x => x.UserId.Equals(userId),
				queryFilter: x => x.FilterByYear(parameters.Year).FilterByMonth(parameters.Month).FilterByDateRange(parameters.MinDate, parameters.MaxDate).FilterByEndOfMonth(parameters.EndOfMonth),
				cancellationToken: cancellationToken);

			IEnumerable <AttendanceResponse> result = _mapper.Map<IEnumerable<AttendanceResponse>>(attendances);

			return new PagedList<AttendanceResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<AttendanceResponse>> GetByDate(int userId, DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			Attendance? attendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarDay.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { $"{nameof(Attendance.CalendarDay)}", $"{nameof(Attendance.DayType)}" }
				);

			if (attendance is null)
				return AttendanceServiceErrors.GetByDateNotFound(date);

			AttendanceResponse result = _mapper.Map<AttendanceResponse>(attendance);

			return result;

		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, new string[] { $"{userId}", $"{date}" }, ex);
			return AttendanceServiceErrors.GetByDateFailed(date);
		}
	}

	public async Task<ErrorOr<AttendanceResponse>> GetById(int userId, int calendarDayId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayId}" };
		try
		{
			Attendance? attendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarDayId.Equals(calendarDayId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { $"{nameof(Attendance.CalendarDay)}", $"{nameof(Attendance.DayType)}" }
				);

			if (attendance is null)
				return AttendanceServiceErrors.GetByIdNotFound(calendarDayId);

			AttendanceResponse result = _mapper.Map<AttendanceResponse>(attendance);

			return result;

		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetByIdFailed(calendarDayId);
		}
	}

	public async Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest updateRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			Attendance? attendance = await _repositoryService.AttendanceRepository
				.GetByIdAsync(id: updateRequest.Id, cancellationToken: cancellationToken);

			if (attendance is null)
				return AttendanceServiceErrors.UpdateNotFound;

			UpdateAttendance(attendance, updateRequest);

			await _repositoryService.AttendanceRepository.UpdateAsync(attendance);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, updateRequest, ex);
			return AttendanceServiceErrors.UpdateFailed;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateMany(IEnumerable<AttendanceUpdateRequest> updateRequest, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<Attendance> attendances = await _repositoryService.AttendanceRepository
				.GetByIdsAsync(ids: updateRequest.Select(x => x.Id), trackChanges: true, cancellationToken: cancellationToken);

			if (!attendances.Any())
				return AttendanceServiceErrors.UpdateManyNotFound;

			foreach (Attendance attendance in attendances)
				UpdateAttendance(attendance, updateRequest.Where(x => x.Id.Equals(attendance.Id)).First());

			await _repositoryService.AttendanceRepository.UpdateAsync(attendances);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_logger.Log(logExceptionWithParams, updateRequest, ex);
			return AttendanceServiceErrors.UpdateManyFailed;
		}
	}

	/// <summary>
	/// Should update the attendance with the update request.
	/// </summary>
	/// <param name="attendance">The attendance to update.</param>
	/// <param name="updateRequest">The request to update with.</param>
	private static void UpdateAttendance(Attendance attendance, AttendanceUpdateRequest updateRequest)
	{
		attendance.DayTypeId = updateRequest.DayTypeId;
		attendance.StartTime = updateRequest.StartTime;
		attendance.EndTime = updateRequest.EndTime;
		attendance.BreakTime = updateRequest.BreakTime;
	}
}
