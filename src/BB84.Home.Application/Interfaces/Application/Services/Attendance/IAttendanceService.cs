using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

namespace BB84.Home.Application.Interfaces.Application.Services.Attendance;

/// <summary>
/// Defines a contract for managing attendance records, including creation, retrieval, updating, and deletion.
/// </summary>
/// <remarks>
/// This service provides methods to handle attendance records for application users.
/// It supports operations such as:
/// <list type="bullet">
/// <item><description>Creating single or multiple attendance entries.</description></item>
/// <item><description>Retrieving attendance records by date or with pagination.</description></item>
/// <item><description>Updating single or multiple attendance entries.</description></item>
/// <item><description>Deleting single or multiple attendance entries.</description></item>
/// </list>
/// All methods return an <see cref="ErrorOr{TValue}"/> result, which encapsulates either the operation's success value or an error.
/// </remarks>
public interface IAttendanceService
{
	/// <summary>
	/// Creates a new attendance record.
	/// </summary>
	/// <remarks>
	/// If an attendance record already exists for the specified user and date, the operation will fail with a conflict error.
	/// </remarks>
	/// <param name="request">The details of the attendance record to be created, including the date and other relevant information.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{Created}"/> result indicating the outcome of the operation.
	/// Returns <see cref="Created"/> if the attendance record is successfully created;
	/// otherwise, returns an error indicating the reason for failure, such as a conflict or an unexpected error.
	/// </returns>
	Task<ErrorOr<Created>> CreateAsync(AttendanceCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates multiple attendance records.
	/// </summary>
	/// <remarks>
	/// This method ensures that no duplicate attendance records are created for the specified user and dates.
	/// If any attendance records already exist for the given dates, the operation will fail with a conflict error.
	/// </remarks>
	/// <param name="requests">A collection of <see cref="AttendanceCreateRequest"/> objects representing the attendance records to be created.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{Created}"/> result indicating the outcome of the operation.
	/// Returns <see cref="Created"/> if the attendance records are successfully created.
	/// If conflicts are detected (e.g., attendance records already exist for the specified dates),
	/// an error containing the conflicting dates is returned.
	/// </returns>
	Task<ErrorOr<Created>> CreateAsync(IEnumerable<AttendanceCreateRequest> requests, CancellationToken token = default);

	/// <summary>
	/// Deletes an attendance record by its unique identifier.
	/// </summary>
	/// <remarks>
	/// If the specified attendance record does not exist, an error is returned.
	/// In the event of an exception during the operation, an error is logged and returned.
	/// </remarks>
	/// <param name="id">The unique identifier of the attendance record to delete.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Deleted"/> if the operation succeeds,
	/// or an error indicating the failure reason.
	/// </returns>
	Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Deletes attendance records corresponding to the specified IDs.
	/// </summary>
	/// <remarks>
	/// This method retrieves the attendance records associated with the provided IDs and deletes them.
	/// If no records are found for the given IDs, an error is returned.
	/// The operation is transactional,  ensuring that changes are committed only if the deletion succeeds.
	/// </remarks>
	/// <param name="ids">A collection of unique identifiers representing the attendance records to delete.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Deleted"/> if the operation succeeds,
	/// or an error if the specified IDs are not found or the deletion fails.
	/// </returns>
	Task<ErrorOr<Deleted>> DeleteAsync(IEnumerable<Guid> ids, CancellationToken token = default);

	/// <summary>
	/// Retrieves a paginated list of attendance records based on the provided filtering parameters.
	/// </summary>
	/// <remarks>
	/// This method retrieves attendance records for the specified user, applying the provided filters and pagination
	/// settings. The results are ordered by the attendance date in ascending order. If the operation fails, an error
	/// is returned instead of the paginated list.
	/// </remarks>
	/// <param name="parameters">
	/// The filtering and pagination parameters used to refine the attendance records.
	/// This includes page number, page size, and any additional filters.
	/// </param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> containing either a paginated list of attendance records (<see cref="IPagedList{T}"/>
	/// of <see cref="AttendanceResponse"/>) or an error indicating the failure reason.
	/// </returns>
	Task<ErrorOr<IPagedList<AttendanceResponse>>> GetPagedByParametersAsync(AttendanceParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Retrieves the attendance record for a given date.
	/// </summary>
	/// <remarks>
	/// This method queries the attendance repository for a record matching the specified user ID and date.
	/// If the record is found, it is mapped to an <see cref="AttendanceResponse"/> and returned.
	/// If no record is found, an error is returned.
	/// </remarks>
	/// <param name="date">The date for which the attendance record is requested. Only the date component is considered.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> containing the attendance record as an <see cref="AttendanceResponse"/> if found.
	/// If no record exists for the specified user and date, returns an error indicating the record was not found.
	/// </returns>
	Task<ErrorOr<AttendanceResponse>> GetByDateAsync(DateTime date, CancellationToken token = default);

	/// <summary>
	/// Updates an existing attendance record with the provided data.
	/// </summary>
	/// <remarks>
	/// This method performs the update operation by mapping the provided request data to the existing attendance record.
	/// Changes are committed to the repository, and any errors during the process are logged.
	/// </remarks>
	/// <param name="request">The request containing the updated attendance data.</param>
	/// <param name="token">A cancellation token that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{T}"/> result containing <see cref="Updated"/> if the update is successful.
	/// If the attendance record is not found, returns an error indicating the record could not be located.
	/// If the update fails due to an exception, returns an error indicating the failure.
	/// </returns>
	Task<ErrorOr<Updated>> UpdateAsync(AttendanceUpdateRequest request, CancellationToken token = default);

	/// <summary>
	/// Updates multiple attendance records based on the provided update requests.
	/// </summary>
	/// <remarks>
	/// This method attempts to update multiple attendance records in a single operation.
	/// If any of the specified records are not found, an error is returned.
	/// The method uses the provided update requests to map new values onto the corresponding attendance entities.
	/// Changes are committed to the database upon successful completion.
	/// </remarks>
	/// <param name="requests">A collection of <see cref="AttendanceUpdateRequest"/> objects
	/// containing the updated data for each attendance record.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>
	/// An <see cref="ErrorOr{Updated}"/> result indicating the outcome of the operation.
	/// Returns <see cref="Updated"/> if the update is successful; otherwise, returns an error indicating the failure reason.
	/// </returns>
	Task<ErrorOr<Updated>> UpdateAsync(IEnumerable<AttendanceUpdateRequest> requests, CancellationToken token = default);
}
