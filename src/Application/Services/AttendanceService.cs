using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Extensions;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Attendance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The attendance service class.
/// </summary>
/// <param name="loggerService">The logger service to use.</param>
/// <param name="repositoryService">The repository service to use.</param>
/// <param name="mapper">The auto mapper to use.</param>
internal sealed class AttendanceService(ILoggerService<AttendanceService> loggerService, IRepositoryService repositoryService, IMapper mapper) : IAttendanceService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> Create(Guid userId, AttendanceCreateRequest request, CancellationToken token = default)
	{
		try
		{
			if (request.IsValid().Equals(false))
				return AttendanceServiceErrors.CreateBadRequest(request.Date);

			AttendanceModel? attendanceEntry = await repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.Date.Equals(request.Date),
				cancellationToken: token
				);

			if (attendanceEntry is not null)
				return AttendanceServiceErrors.CreateConflict(request.Date);

			AttendanceModel newAttendance = mapper.Map<AttendanceModel>(request);
			newAttendance.UserId = userId;

			await repositoryService.AttendanceRepository.CreateAsync(newAttendance, token);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Created;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<AttendanceCreateRequest> requests, CancellationToken token = default)
	{
		try
		{
			ErrorOr<Created> response = new();

			foreach (AttendanceCreateRequest request in requests)
				if (request.IsValid().Equals(false))
					response.Errors.Add(AttendanceServiceErrors.CreateBadRequest(request.Date));

			if (response.IsError)
				return response;

			IEnumerable<AttendanceModel> attendanceEntries = await repositoryService.AttendanceRepository
				.GetManyByConditionAsync(expression: x => x.UserId.Equals(userId) && requests.Select(x => x.Date).Contains(x.Date), cancellationToken: token)
				.ConfigureAwait(false);

			foreach (AttendanceModel attendanceEntry in attendanceEntries)
				response.Errors.Add(AttendanceServiceErrors.CreateConflict(attendanceEntry.Date));

			if (response.IsError)
				return response;

			List<AttendanceModel> newAttendances = [];

			foreach (AttendanceCreateRequest request in requests)
			{
				AttendanceModel newAttendance = mapper.Map<AttendanceModel>(request);
				newAttendance.UserId = userId;
				newAttendances.Add(newAttendance);
			}

			await repositoryService.AttendanceRepository.CreateAsync(newAttendances, token);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", string.Join(";", requests.Select(x => x.Date))];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.CreateManyFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, DateTime date, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", $"{date}"];
		try
		{
			AttendanceModel? attendanceEntry = await repositoryService.AttendanceRepository
				.GetByConditionAsync(expression: x => x.UserId.Equals(userId) && x.Date.Equals(date.Date), cancellationToken: token)
				.ConfigureAwait(false);

			if (attendanceEntry is null)
				return AttendanceServiceErrors.GetByDateFailed(date);

			await repositoryService.AttendanceRepository.DeleteAsync(attendanceEntry);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> Delete(Guid userId, IEnumerable<DateTime> dates, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", string.Join(", ", dates)];
		try
		{
			IEnumerable<AttendanceModel> attendanceEntries = await repositoryService.AttendanceRepository
				.GetManyByConditionAsync(expression: x => dates.Contains(x.Date) && x.UserId.Equals(userId), cancellationToken: token)
				.ConfigureAwait(false);

			if (attendanceEntries.Any().Equals(false))
				return AttendanceServiceErrors.DeleteManyNotFound;

			await repositoryService.AttendanceRepository.DeleteAsync(attendanceEntries);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteManyFailed;
		}
	}

	public async Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedListByParameters(Guid userId, AttendanceParameters parameters, CancellationToken token = default)
	{
		try
		{
			IEnumerable<AttendanceModel> attendances = await repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				queryFilter: x => x.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
				.FilterByType(parameters.AttendanceType),
				orderBy: x => x.OrderBy(x => x.Date),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				cancellationToken: token
				);

			if (!attendances.Any())
				return AttendanceServiceErrors.GetPagedByParametersNotFound;

			int totalCount = await repositoryService.AttendanceRepository.CountAsync(
				expression: x => x.UserId.Equals(userId),
				queryFilter: x => x.FilterByYear(parameters.Year)
				.FilterByMonth(parameters.Month)
				.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
				.FilterByType(parameters.AttendanceType),
				cancellationToken: token);

			IEnumerable<AttendanceResponse> result = mapper.Map<IEnumerable<AttendanceResponse>>(attendances);

			return new PagedList<AttendanceResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<AttendanceResponse>> GetByDate(Guid userId, DateTime date, CancellationToken token = default)
	{
		string[] parameters = [$"{userId}", $"{date}"];
		try
		{
			AttendanceModel? attendanceEntry = await repositoryService.AttendanceRepository
				.GetByConditionAsync(expression: x => x.UserId.Equals(userId) && x.Date.Equals(date.Date), cancellationToken: token)
				.ConfigureAwait(false);

			if (attendanceEntry is null)
				return AttendanceServiceErrors.GetByDateNotFound(date);

			AttendanceResponse result = mapper.Map<AttendanceResponse>(attendanceEntry);

			return result;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetByDateFailed(date);
		}
	}

	public async Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			if (request.IsValid().Equals(false))
				return AttendanceServiceErrors.UpdateBadRequest(request.Id);

			AttendanceModel? attendanceEntry = await repositoryService.AttendanceRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(request.Id),
				trackChanges: true,
				cancellationToken: token
				);

			if (attendanceEntry is null)
				return AttendanceServiceErrors.UpdateNotFound;

			UpdateAttendance(attendanceEntry, request);

			await repositoryService.AttendanceRepository.UpdateAsync(attendanceEntry);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.UpdateFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(IEnumerable<AttendanceUpdateRequest> requests, CancellationToken token = default)
	{
		string[] parameters = [string.Join(";", requests.Select(x => x.Id))];
		try
		{
			ErrorOr<Updated> response = new();

			foreach (AttendanceUpdateRequest request in requests)
				if (request.IsValid().Equals(false))
					response.Errors.Add(AttendanceServiceErrors.UpdateBadRequest(request.Id));

			if (response.IsError)
				return response;

			IEnumerable<AttendanceModel> attendanceEntries = await repositoryService.AttendanceRepository.GetManyByConditionAsync(
				expression: x => requests.Select(x => x.Id).Contains(x.Id),
				trackChanges: true,
				cancellationToken: token
				);

			if (attendanceEntries.Any().Equals(false))
				return AttendanceServiceErrors.UpdateManyNotFound;

			foreach (AttendanceModel attendanceEntry in attendanceEntries)
				UpdateAttendance(attendanceEntry, requests.Where(x => x.Id.Equals(attendanceEntry.Id)).First());

			await repositoryService.AttendanceRepository.UpdateAsync(attendanceEntries);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.UpdateManyFailed;
		}
	}

	/// <summary>
	/// Updates the attendance with the update request.
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
