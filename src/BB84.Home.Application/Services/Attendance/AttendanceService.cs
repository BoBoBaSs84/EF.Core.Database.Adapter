﻿using AutoMapper;

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
/// <param name="mapper">The mapper for converting between domain entities and data transfer objects.</param>
internal sealed class AttendanceService(ILoggerService<AttendanceService> loggerService, ICurrentUserService userService, IRepositoryService repositoryService, IMapper mapper) : IAttendanceService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateAsync(AttendanceCreateRequest request, CancellationToken token = default)
	{
		try
		{
			AttendanceEntity? entity = await repositoryService.AttendanceRepository
				.GetByConditionAsync(expression: x => x.Date.Equals(request.Date), token: token)
				.ConfigureAwait(false);

			if (entity is not null)
				return AttendanceServiceErrors.CreateConflict(request.Date);

			AttendanceEntity newEntity = mapper.Map<AttendanceEntity>(request);
			newEntity.UserId = userService.UserId;

			await repositoryService.AttendanceRepository.CreateAsync(newEntity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
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
			IEnumerable<AttendanceEntity> entities = await repositoryService.AttendanceRepository
				.GetManyByConditionAsync(expression: x => requests.Select(x => x.Date).Contains(x.Date), token: token)
				.ConfigureAwait(false);

			if (entities.Any())
				return AttendanceServiceErrors.CreateMultipleConflict(entities.Select(x => x.Date));

			List<AttendanceEntity> newEntities = [];

			foreach (AttendanceCreateRequest request in requests)
			{
				AttendanceEntity newAttendance = mapper.Map<AttendanceEntity>(request);
				newAttendance.UserId = userService.UserId;
				newEntities.Add(newAttendance);
			}

			await repositoryService.AttendanceRepository.CreateAsync(newEntities, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
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
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return AttendanceServiceErrors.GetByIdNotFound(id);

			await repositoryService.AttendanceRepository
				.DeleteAsync(entity, token)
				.ConfigureAwait(false);

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
			IEnumerable<AttendanceEntity> entities = await repositoryService.AttendanceRepository
				.GetByIdsAsync(ids, token: token)
				.ConfigureAwait(false);

			if (entities.Any().IsFalse())
				return AttendanceServiceErrors.GetByIdsNotFound(ids);

			await repositoryService.AttendanceRepository
				.DeleteAsync(entities, token)
				.ConfigureAwait(false);

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
			IEnumerable<AttendanceEntity> attendances = await repositoryService.AttendanceRepository
				.GetManyByConditionAsync(
					queryFilter: x => x.FilterByParameters(parameters),
					orderBy: x => x.OrderBy(x => x.Date),
					skip: (parameters.PageNumber - 1) * parameters.PageSize,
					take: parameters.PageSize,
					token: token)
				.ConfigureAwait(false);

			int totalCount = await repositoryService.AttendanceRepository
				.CountAsync(
					queryFilter: x => x.FilterByParameters(parameters),
					token: token)
				.ConfigureAwait(false);

			IEnumerable<AttendanceResponse> result = mapper.Map<IEnumerable<AttendanceResponse>>(attendances);

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
				.GetByConditionAsync(expression: x => x.Date.Equals(date.Date), token: token)
				.ConfigureAwait(false);

			if (attendanceEntry is null)
				return AttendanceServiceErrors.GetByDateNotFound(date);

			AttendanceResponse result = mapper.Map<AttendanceResponse>(attendanceEntry);

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
				.GetByIdAsync(request.Id, trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				return AttendanceServiceErrors.GetByIdNotFound(request.Id);

			_ = mapper.Map(request, entity);

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
				.GetByIdsAsync(requests.Select(x => x.Id), trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (entities.Any().IsFalse())
				return AttendanceServiceErrors.GetByIdsNotFound(requests.Select(x => x.Id));

			foreach (AttendanceEntity entity in entities)
				_ = mapper.Map(requests.Single(x => x.Id.Equals(entity.Id)), entity);

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
