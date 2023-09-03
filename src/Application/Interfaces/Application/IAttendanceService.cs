using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The attendance service interface.
/// </summary>
[SuppressMessage("Naming", "CA1716", Justification = "Usable in all available languages in .NET")]
public interface IAttendanceService
{
	/// <summary>
	/// Creates an attendance for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="createRequest">The attendance create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, AttendanceCreateRequest createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates multiple attendances for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="createRequest">The attendances create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	/// <returns></returns>
	Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<AttendanceCreateRequest> createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an attendance for the given <paramref name="userId"/> by the calendar day identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="calendarDayId">The calendar day identifier to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, Guid calendarDayId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes multiple attendances for the given <paramref name="userId"/> by the calendar day identifiers.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="calendarDayIds">The calendar day identifiers to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, IEnumerable<Guid> calendarDayIds, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns multiple attendances as a paged list for the given <paramref name="userId"/> filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> Get(Guid userId, AttendanceParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the attendance for the given <paramref name="userId"/> by the calendar day date.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="date">The calendar day date.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceResponse>> Get(Guid userId, DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the attendance for the given <paramref name="userId"/> by calendar day identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="calendarDayId"></param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceResponse>> Get(Guid userId, Guid calendarDayId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates the existing attendance.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest updateRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates multiple existing attendances.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(IEnumerable<AttendanceUpdateRequest> updateRequest, CancellationToken cancellationToken = default);
}
