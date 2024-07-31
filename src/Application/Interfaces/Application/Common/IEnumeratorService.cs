﻿using Application.Contracts.Responses.Common;

using Domain.Errors;

namespace Application.Interfaces.Application.Common;

/// <summary>
/// The enumerator service interface.
/// </summary>
public interface IEnumeratorService
{
	/// <summary>
	/// Returns all card type enumerators.
	/// </summary>
	/// <returns>A list of card type responses.</returns>
	ErrorOr<IEnumerable<CardTypeResponse>> GetCardTypes();

	/// <summary>
	/// Returns all attendance type enumerators.
	/// </summary>
	/// <returns>A list of attendance type responses.</returns>
	ErrorOr<IEnumerable<AttendanceTypeResponse>> GetAttendanceTypes();

	/// <summary>
	/// Returns all priority level type enumerators.
	/// </summary>
	/// <returns>A list of priority level type responses.</returns>
	ErrorOr<IEnumerable<PriorityLevelTypeResponse>> GetPriorityLevelTypes();

	/// <summary>
	/// Returns all role type enumerators.
	/// </summary>
	/// <returns>A list of role type responses.</returns>
	ErrorOr<IEnumerable<RoleTypeResponse>> GetRoleTypes();

	/// <summary>
	/// Returns all work day type enumerators.
	/// </summary>
	/// <returns>A list of work day type responses.</returns>
	ErrorOr<IEnumerable<WorkDayTypeResponse>> GetWorkDayTypes();
}
