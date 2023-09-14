using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Attendance;
using Domain.Models.Common;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The attendance service class.
/// </summary>
internal sealed class AttendanceService : IAttendanceService
{
	private readonly ILoggerService<AttendanceService> _loggerService;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	/// <summary>
	/// Initilizes an instance of the attendance service class.
	/// </summary>
	/// <param name="loggerService">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public AttendanceService(ILoggerService<AttendanceService> loggerService, IRepositoryService repositoryService, IMapper mapper)
	{
		_loggerService = loggerService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, AttendanceCreateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			CalendarModel? calendarModel = await _repositoryService.CalendarRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(request.Date),
				cancellationToken: cancellationToken
				);

			if (calendarModel is null)
				return AttendanceServiceErrors.CreateNotFound(request.Date);

			AttendanceModel? attendanceModel = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x=>x.Calendar.Date.Equals(request.Date),
				cancellationToken: cancellationToken
				);

			if (attendanceModel is not null)
				return AttendanceServiceErrors.CreateConflict(request.Date);

			AttendanceModel newAttendance = _mapper.Map<AttendanceModel>(request);
			newAttendance.UserId = userId;
			newAttendance.CalendarId = calendarModel.Id;

			await _repositoryService.AttendanceRepository.CreateAsync(newAttendance, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<AttendanceCreateRequest> request, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CalendarModel> calendarEntries = await _repositoryService.CalendarRepository.GetManyByConditionAsync(
				expression: x => request.Select(x => x.Date).Contains(x.Date),
				cancellationToken: cancellationToken
				);

			// Todo
			if (request.Count() != calendarEntries.Count())
				return AttendanceServiceErrors.CreateFailed;

			List<AttendanceModel> newAttendances = new();

			foreach (CalendarModel calendarEntry in calendarEntries)
			{
				AttendanceCreateRequest createRequest = request.Where(x => x.Date.Equals(calendarEntry.Date)).First();
				AttendanceModel attendance = _mapper.Map<AttendanceModel>(createRequest);
				attendance.CalendarId = calendarEntry.Id;
				newAttendances.Add(attendance);
			}

			await _repositoryService.AttendanceRepository.CreateAsync(newAttendances, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.CreateManyFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, Guid calendarDayId, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayId}" };
		try
		{
			AttendanceModel? dbAttendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarId.Equals(calendarDayId),
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
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, IEnumerable<Guid> calendarDayIds, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayIds.ToJsonString()}" };
		try
		{
			IEnumerable<AttendanceModel> attendances = await _repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => calendarDayIds.Contains(x.CalendarId) && x.UserId.Equals(userId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (!attendances.Any())
				return AttendanceServiceErrors.DeleteManyNotFound;

			await _repositoryService.AttendanceRepository.DeleteAsync(attendances);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteManyFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<AttendanceResponse>>> Get(Guid userId, AttendanceParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<AttendanceModel> attendances = await _repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				queryFilter: x => x.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
				.FilterByType(parameters.AttendanceType),
				orderBy: x => x.OrderBy(x => x.Calendar.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AttendanceModel.Calendar) }
				);

			if (!attendances.Any())
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await _repositoryService.AttendanceRepository.GetCountAsync(
				expression: x => x.UserId.Equals(userId),
				queryFilter: x => x.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
				.FilterByType(parameters.AttendanceType),
				cancellationToken: cancellationToken);

			IEnumerable<AttendanceResponse> result = _mapper.Map<IEnumerable<AttendanceResponse>>(attendances);

			return new PagedList<AttendanceResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<AttendanceResponse>> Get(Guid userId, DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			AttendanceModel? attendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.Calendar.Date.Equals(date.ToSqlDate()),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AttendanceModel.Calendar) }
				);

			if (attendance is null)
				return AttendanceServiceErrors.GetByDateNotFound(date);

			AttendanceResponse result = _mapper.Map<AttendanceResponse>(attendance);

			return result;

		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, new string[] { $"{userId}", $"{date}" }, ex);
			return AttendanceServiceErrors.GetByDateFailed(date);
		}
	}

	public async Task<ErrorOr<AttendanceResponse>> Get(Guid userId, Guid calendarDayId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		string[] parameters = new string[] { $"{userId}", $"{calendarDayId}" };
		try
		{
			AttendanceModel? attendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId) && x.CalendarId.Equals(calendarDayId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken,
				includeProperties: new[] { nameof(AttendanceModel.Calendar) }
				);

			if (attendance is null)
				return AttendanceServiceErrors.GetByIdNotFound(calendarDayId);

			AttendanceResponse result = _mapper.Map<AttendanceResponse>(attendance);

			return result;

		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetByIdFailed(calendarDayId);
		}
	}

	public async Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			AttendanceModel? attendance = await _repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (attendance is null)
				return AttendanceServiceErrors.UpdateNotFound;

			UpdateAttendance(attendance, request);

			await _repositoryService.AttendanceRepository.UpdateAsync(attendance);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.UpdateFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(IEnumerable<AttendanceUpdateRequest> request, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<AttendanceModel> attendances = await _repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => request.Select(x => x.Id).Contains(x.Id),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (!attendances.Any())
				return AttendanceServiceErrors.UpdateManyNotFound;

			foreach (AttendanceModel attendance in attendances)
				UpdateAttendance(attendance, request.Where(x => x.Id.Equals(attendance.Id)).First());

			await _repositoryService.AttendanceRepository.UpdateAsync(attendances);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.UpdateManyFailed;
		}
	}

	/// <summary>
	/// Should update the attendance with the update request.
	/// </summary>
	/// <param name="attendance">The attendance to update.</param>
	/// <param name="updateRequest">The request to update with.</param>
	private static void UpdateAttendance(AttendanceModel attendance, AttendanceUpdateRequest updateRequest)
	{
		attendance.AttendanceType = updateRequest.AttendanceType;
		attendance.StartTime = updateRequest.StartTime;
		attendance.EndTime = updateRequest.EndTime;
		attendance.BreakTime = updateRequest.BreakTime;
	}
}
