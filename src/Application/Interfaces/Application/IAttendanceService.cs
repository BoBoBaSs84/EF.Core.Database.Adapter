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
	/// Creates an attendance for for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The attendance create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, AttendanceCreateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates multiple attendances for the for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="requests">The attendance create requests.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	/// <returns></returns>
	Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<AttendanceCreateRequest> requests, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an attendance for the application user and the calendar date.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="date">The calendar date to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, DateTime date, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes multiple attendances for the application user and the dates.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="dates">The calendar dates to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, IEnumerable<DateTime> dates, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns multiple attendances as a paged list for the application user filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> Get(Guid userId, AttendanceParameters parameters, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a attendance report as a paged list for the application user filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <remarks>
	/// Attendances and absences are output in a calendar-like form.
	/// </remarks>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> Get(Guid userId, CalendarParameters parameters, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the attendance for the application user and the calendar entry date.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="date">The calendar day date.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceResponse>> Get(Guid userId, DateTime date, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates the existing attendance.
	/// </summary>
	/// <param name="request">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates multiple existing attendances.
	/// </summary>
	/// <param name="requests">The attendance update requests.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(IEnumerable<AttendanceUpdateRequest> requests, CancellationToken cancellationToken = default);
}
