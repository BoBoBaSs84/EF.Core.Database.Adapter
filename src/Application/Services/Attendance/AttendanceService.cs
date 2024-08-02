using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Extensions;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Attendance;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Extensions;
using Domain.Models.Attendance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services.Attendance;

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

	public async Task<ErrorOr<Created>> Create(Guid id, AttendanceCreateRequest request, CancellationToken token = default)
	{
		try
		{
			if (request.IsValid().Equals(false))
				return AttendanceServiceErrors.CreateBadRequest(request.Date);

			AttendanceModel? attendanceEntry = await repositoryService.AttendanceRepository
				.GetByConditionAsync(expression: x => x.UserId.Equals(id) && x.Date.Equals(request.Date), cancellationToken: token)
				.ConfigureAwait(false);

			if (attendanceEntry is not null)
				return AttendanceServiceErrors.CreateConflict(request.Date);

			AttendanceModel newAttendance = mapper.Map<AttendanceModel>(request);
			newAttendance.UserId = id;

			await repositoryService.AttendanceRepository.CreateAsync(newAttendance, token)
				.ConfigureAwait(false);

			await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{id}", $"{request.Date}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.CreateFailed(request.Date);
		}
	}

	public async Task<ErrorOr<Created>> CreateMultiple(Guid id, IEnumerable<AttendanceCreateRequest> requests, CancellationToken token = default)
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
				.GetManyByConditionAsync(expression: x => x.UserId.Equals(id) && requests.Select(x => x.Date).Contains(x.Date), cancellationToken: token)
				.ConfigureAwait(false);

			foreach (AttendanceModel attendanceEntry in attendanceEntries)
				response.Errors.Add(AttendanceServiceErrors.CreateConflict(attendanceEntry.Date));

			if (response.IsError)
				return response;

			List<AttendanceModel> newAttendances = [];

			foreach (AttendanceCreateRequest request in requests)
			{
				AttendanceModel newAttendance = mapper.Map<AttendanceModel>(request);
				newAttendance.UserId = id;
				newAttendances.Add(newAttendance);
			}

			await repositoryService.AttendanceRepository.CreateAsync(newAttendances, token);
			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{id}", string.Join(";", requests.Select(x => x.Date))];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.CreateManyFailed;
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteById(Guid id, CancellationToken token = default)
	{
		try
		{
			AttendanceModel? record = await repositoryService.AttendanceRepository
				.GetByIdAsync(id, cancellationToken: token)
				.ConfigureAwait(false);

			if (record is null)
				return AttendanceServiceErrors.GetByIdNotFound(id);

			await repositoryService.AttendanceRepository.DeleteAsync(record);

			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return AttendanceServiceErrors.DeleteByIdFailed(id);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByIds(IEnumerable<Guid> ids, CancellationToken token = default)
	{
		try
		{
			IEnumerable<AttendanceModel> records = await repositoryService.AttendanceRepository
				.GetByIdsAsync(ids, cancellationToken: token)
				.ConfigureAwait(false);

			if (records.Any().Equals(false))
				return AttendanceServiceErrors.GetByIdsNotFound(ids);

			await repositoryService.AttendanceRepository.DeleteAsync(records);

			_ = await repositoryService.CommitChangesAsync(token);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{string.Join(", ", ids)}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteByIdsFailed(ids);
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
			string[] parameters = [$"{userId}", $"{date}"];
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

			AttendanceModel? attendanceEntry = await repositoryService.AttendanceRepository
				.GetByIdAsync(request.Id, trackChanges: true, cancellationToken: token)
				.ConfigureAwait(false);

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
		
		try
		{
			ErrorOr<Updated> response = new();

			foreach (AttendanceUpdateRequest request in requests)
				if (request.IsValid().Equals(false))
					response.Errors.Add(AttendanceServiceErrors.UpdateBadRequest(request.Id));

			if (response.IsError)
				return response;

			IEnumerable<AttendanceModel> attendanceEntries = await repositoryService.AttendanceRepository
				.GetByIdsAsync(requests.Select(x => x.Id), trackChanges: true, cancellationToken: token)
				.ConfigureAwait(false);

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
			string[] parameters = [string.Join(',', requests.Select(x => x.Id))];
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
