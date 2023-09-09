using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Errors;
using Domain.Models.Attendance;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The attendance settings service class.
/// </summary>
internal sealed class AttendanceSettingsService : IAttendanceSettingsService
{
	private readonly ILoggerService<AttendanceSettingsService> _loggerService;
	private readonly IRepositoryService _repositoryService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initilizes an instance of the attendance settings service class.
	/// </summary>
	/// <param name="loggerService">The logger service to use.</param>
	/// <param name="repositoryService">The repository service to use.</param>
	/// <param name="mapper">The auto mapper to use.</param>
	public AttendanceSettingsService(ILoggerService<AttendanceSettingsService> loggerService, IRepositoryService repositoryService, IMapper mapper)
	{
		_loggerService = loggerService;
		_repositoryService = repositoryService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, AttendanceSettingsRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			AttendanceSettingsModel? attendanceSettings = await _repositoryService.AttendanceSettingsRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				cancellationToken: cancellationToken
				);

			if (attendanceSettings is not null)
				return AttendanceSettingsServiceErrors.CreateConflict;

			attendanceSettings = _mapper.Map<AttendanceSettingsModel>(request);
			attendanceSettings.UserId = userId;

			await _repositoryService.AttendanceSettingsRepository.CreateAsync(attendanceSettings, cancellationToken);
			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Created;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return AttendanceSettingsServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<AttendanceSettingsResponse>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			AttendanceSettingsModel? attendanceSettings = await _repositoryService.AttendanceSettingsRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (attendanceSettings is null)
				return AttendanceSettingsServiceErrors.GetNotFound;

			AttendanceSettingsResponse result = _mapper.Map<AttendanceSettingsResponse>(attendanceSettings);

			return result;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return AttendanceSettingsServiceErrors.CreateFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, AttendanceSettingsRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			AttendanceSettingsModel? attendanceSettings = await _repositoryService.AttendanceSettingsRepository.GetByConditionAsync(
				expression: x => x.UserId.Equals(userId),
				trackChanges: true,
				cancellationToken: cancellationToken
				);

			if (attendanceSettings is null)
				return AttendanceSettingsServiceErrors.GetNotFound;

			attendanceSettings.WorkHours = request.WorkHours;
			attendanceSettings.WorkDays = request.WorkDays;

			_ = await _repositoryService.CommitChangesAsync(cancellationToken);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return AttendanceSettingsServiceErrors.UpdateFailed;
		}
	}
}
