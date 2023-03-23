using Application.Contracts.Requests;
using Application.Contracts.Responses;
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
	/// Should create a attendance.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="createRequest">The attendance create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(int userId, AttendanceCreateRequest createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should create multiple attendances.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="createRequest">The attendances create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	/// <returns></returns>
	Task<ErrorOr<Created>> CreateMany(int userId, IEnumerable<AttendanceCreateRequest> createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should delete a attendance by the calendat day identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="calendarDayId">The calendar day identifier to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(int userId, int calendarDayId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should delete multiple attendances by the calendar day identifiers.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="calendarDayIds">The calendar day identifiers to delete.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteMany(int userId, IEnumerable<int> calendarDayIds, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the attendance entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParameters(int userId, AttendanceParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the attendance entity by the calendar day date.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="date">The calendar day date.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceResponse>> GetByDate(int userId, DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the attendance entity by calendar day identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="calendarDayId"></param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AttendanceResponse>> GetById(int userId, int calendarDayId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should update a attendance.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(AttendanceUpdateRequest updateRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should update multiple attendances.
	/// </summary>
	/// <param name="updateRequest">The attendance update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateMany(IEnumerable<AttendanceUpdateRequest> updateRequest, CancellationToken cancellationToken = default);
}
