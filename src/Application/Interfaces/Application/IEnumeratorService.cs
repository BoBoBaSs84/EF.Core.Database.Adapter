using Application.Contracts.Responses.Enumerators;

using Domain.Errors;

namespace Application.Interfaces.Application;

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
	/// Returns all day type enumerators.
	/// </summary>
	/// <returns>A list of day type responses.</returns>
	ErrorOr<IEnumerable<DayTypeResponse>> GetDayTypes();

	/// <summary>
	/// Returns all role type enumerators.
	/// </summary>
	/// <returns>A list of role type responses.</returns>
	ErrorOr<IEnumerable<RoleTypeResponse>> GetRoleTypes();
}
