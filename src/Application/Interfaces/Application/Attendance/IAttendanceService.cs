using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application.Attendance;

/// <summary>
/// The interface for the attendance service.
/// </summary>
public interface IAttendanceService
{
	/// <summary>
	/// Creates a new attendance entry for the user with the provided create <paramref name="request"/>.
	/// </summary>
	/// <param name="id">The identifier of the application user.</param>
	/// <param name="request">The attendance create request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid id, AttendanceCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates multiple new attendance entries for the user with the provided create <paramref name="requests"/>.
	/// </summary>
	/// <param name="id">The identifier of the application user.</param>
	/// <param name="requests">The attendance create requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateMultiple(Guid id, IEnumerable<AttendanceCreateRequest> requests, CancellationToken token = default);

	/// <summary>
	/// Deletes an attendance entry by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The attendance entry identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteById(Guid id, CancellationToken token = default);

	/// <summary>
	/// Deletes multiple attendance entries by the provided <paramref name="ids"/>.
	/// </summary>
	/// <param name="ids">The attendance entry identifiers to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteByIds(IEnumerable<Guid> ids, CancellationToken token = default);

	/// <summary>
	/// Returns multiple attendances as a paged list for the application user filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParameters(Guid userId, AttendanceParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Returns the attendance for the application user and the calendar entry date.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="date">The calendar day date.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceResponse>> GetByDate(Guid userId, DateTime date, CancellationToken token = default);

	/// <summary>
	/// Updates the existing attendance.
	/// </summary>
	/// <param name="request">The attendance update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest request, CancellationToken token = default);

	/// <summary>
	/// Updates multiple existing attendances.
	/// </summary>
	/// <param name="requests">The attendance update requests.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateMultiple(IEnumerable<AttendanceUpdateRequest> requests, CancellationToken token = default);
}
