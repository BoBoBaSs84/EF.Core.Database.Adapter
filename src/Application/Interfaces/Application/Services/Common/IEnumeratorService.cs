using Application.Contracts.Responses.Common;

using BB84.Home.Domain.Errors;

namespace Application.Interfaces.Application.Services.Common;

/// <summary>
/// The enumerator service interface.
/// </summary>
public interface IEnumeratorService
{
	/// <summary>
	/// Returns all bank account type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<AccountTypeResponse>> GetAccountTypes();

	/// <summary>
	/// Returns all attendance type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<AttendanceTypeResponse>> GetAttendanceTypes();

	/// <summary>
	/// Returns all bank card type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<CardTypeResponse>> GetCardTypes();

	/// <summary>
	/// Returns all document type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<DocumentTypeResponse>> GetDocumentTypes();

	/// <summary>
	/// Returns all priority level type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<PriorityLevelTypeResponse>> GetPriorityLevelTypes();

	/// <summary>
	/// Returns all user role type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<RoleTypeResponse>> GetRoleTypes();

	/// <summary>
	/// Returns all work day type enumerators.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IEnumerable<WorkDayTypeResponse>> GetWorkDayTypes();
}
