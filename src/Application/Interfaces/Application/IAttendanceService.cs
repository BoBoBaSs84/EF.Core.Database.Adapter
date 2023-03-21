using Application.Contracts.Responses;
using Application.Features.Requests;
using Application.Features.Responses;
using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The attendance service interface.
/// </summary>
public interface IAttendanceService
{
	/// <summary>
	/// Should return the attendance entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParameters(int userId, AttendanceParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);
}
