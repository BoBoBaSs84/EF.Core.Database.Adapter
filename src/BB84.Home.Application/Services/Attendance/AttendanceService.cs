using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Extensions;
using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Application.Services.Attendance;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Attendance;

/// <summary>
/// Provides functionality for managing attendance records, including creation, retrieval, updating, and deletion.
/// </summary>
/// <param name="loggerService">The logger service for logging errors and information.</param>
/// <param name="userService"> The service providing information about the current user.</param>
/// <param name="repositoryService">The repository service for accessing data repositories.</param>
internal sealed class AttendanceService(ILoggerService<AttendanceService> loggerService, ICurrentUserService userService, IRepositoryService repositoryService) : IAttendanceService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateAsync(AttendanceCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AttendanceEntity? entity = await repositoryService.AttendanceRepository
				.GetSingleAsync(new() { Where = x => x.Date.Equals(request.Date) }, token)
				.ConfigureAwait(false);

			if (entity is not null)
				return AttendanceServiceErrors.CreateConflict(request.Date);

			AttendanceEntity newEntity = request.ToEntity(userService.UserId);

			await repositoryService.AttendanceRepository
				.CreateAsync(newEntity, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userService.UserId}", $"{request.Date}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.CreateFailed(request.Date);
		}
	}

	public async Task<ErrorOr<Created>> CreateAsync(IEnumerable<AttendanceCreateRequest> requests, CancellationToken token = default)
	{
		try
		{
			IReadOnlyList<AttendanceEntity> entities = await repositoryService.AttendanceRepository
				.GetListAsync(new() { Where = x => requests.Select(x => x.Date).Contains(x.Date) }, token)
				.ConfigureAwait(false);

			if (entities.Count > 0)
				return AttendanceServiceErrors.CreateMultipleConflict(entities.Select(x => x.Date));

			List<AttendanceEntity> newEntities = [];

			foreach (AttendanceCreateRequest request in requests)
			{
				AttendanceEntity newAttendance = request.ToEntity(userService.UserId);
				newEntities.Add(newAttendance);
			}

			await repositoryService.AttendanceRepository
				.CreateAsync(newEntities, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userService.UserId}", string.Join(',', requests.Select(x => x.Date))];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.CreateMultipleFailed(requests.Select(x => x.Date));
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default)
	{
		try
		{
			AttendanceEntity? entity = await repositoryService.AttendanceRepository
				.GetByIdAsync(id, cancellationToken: token)
				.ConfigureAwait(false);

			if (entity is null)
				return AttendanceServiceErrors.GetByIdNotFound(id);

			repositoryService.AttendanceRepository
				.Delete(entity);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, id, ex);
			return AttendanceServiceErrors.DeleteByIdFailed(id);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteAsync(IEnumerable<Guid> ids, CancellationToken token = default)
	{
		try
		{
			IReadOnlyList<AttendanceEntity> entities = await repositoryService.AttendanceRepository
				.GetByIdsAsync(ids, cancellationToken: token)
				.ConfigureAwait(false);

			if (entities.Count.Equals(0))
				return AttendanceServiceErrors.GetByIdsNotFound(ids);

			repositoryService.AttendanceRepository
				.Delete(entities);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{string.Join(',', ids)}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.DeleteByIdsFailed(ids);
		}
	}

	public async Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParametersAsync(AttendanceParameters parameters, CancellationToken token = default)
	{
		try
		{
			Query<AttendanceEntity> query = new()
			{
				QueryFilter = x => x.FilterByParameters(parameters),
				OrderBy = x => x.OrderBy(x => x.Date),
				Skip = (parameters.PageNumber - 1) * parameters.PageSize,
				Take = parameters.PageSize
			};

			IReadOnlyList<AttendanceEntity> attendances = await repositoryService.AttendanceRepository
				.GetListAsync(query, token)
				.ConfigureAwait(false);

			int totalCount = await repositoryService.AttendanceRepository
				.CountAsync(new() { QueryFilter = x => x.FilterByParameters(parameters) }, token)
				.ConfigureAwait(false);

			IEnumerable<AttendanceResponse> result = attendances.Select(x => x.ToResponse());

			return new PagedList<AttendanceResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<AttendanceResponse>> GetByDateAsync(DateTime date, CancellationToken token = default)
	{
		try
		{
			AttendanceEntity? attendanceEntry = await repositoryService.AttendanceRepository
				.GetSingleAsync(new() { Where = x => x.Date.Equals(date.Date) }, token)
				.ConfigureAwait(false);

			if (attendanceEntry is null)
				return AttendanceServiceErrors.GetByDateNotFound(date);

			AttendanceResponse result = attendanceEntry.ToResponse();

			return result;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userService.UserId}", $"{date}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.GetByDateFailed(date);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateAsync(AttendanceUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			AttendanceEntity? entity = await repositoryService.AttendanceRepository
				.GetByIdAsync(request.Id, new() { TrackChanges = true }, token)
				.ConfigureAwait(false);

			if (entity is null)
				return AttendanceServiceErrors.GetByIdNotFound(request.Id);

			entity = request.ToEntity(entity);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, request, ex);
			return AttendanceServiceErrors.UpdateFailed(request.Id);
		}
	}

	public async Task<ErrorOr<Updated>> UpdateAsync(IEnumerable<AttendanceUpdateRequest> requests, CancellationToken token = default)
	{
		try
		{
			IEnumerable<AttendanceEntity> entities = await repositoryService.AttendanceRepository
				.GetByIdsAsync(requests.Select(x => x.Id), new() { TrackChanges = true }, token)
				.ConfigureAwait(false);

			if (entities.Any().IsFalse())
				return AttendanceServiceErrors.GetByIdsNotFound(requests.Select(x => x.Id));

			foreach (AttendanceEntity entity in entities)
				_ = requests.Single(x => x.Id.Equals(entity.Id)).ToEntity(entity);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			string[] parameters = [string.Join(',', requests.Select(x => x.Id))];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			return AttendanceServiceErrors.UpdateMultipleFailed(requests.Select(x => x.Id));
		}
	}
}
