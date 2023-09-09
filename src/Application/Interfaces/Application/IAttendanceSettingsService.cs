using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The attendance settings service interface.
/// </summary>
public interface IAttendanceSettingsService
{
	/// <summary>
	/// Creates new attendance settings for for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The attendances create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, AttendanceSettingsRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the attendance settings for for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceSettingsResponse>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates existing attendance settings for for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The attendances create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid userId, AttendanceSettingsRequest request, CancellationToken cancellationToken = default);
}
