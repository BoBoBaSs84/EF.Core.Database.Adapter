using Domain.Enumerators;
using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The day type service interface.
/// </summary>
public interface IDayTypeService
{
	/// <summary>
	/// Returns all day type entities.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A paged list response.</returns>
	Task<ErrorOr<IEnumerable<DayType>>> Get(bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the day type by its <paramref name="name"/>.
	/// </summary>
	/// <param name="name">The name of the day type.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns></returns>
	Task<ErrorOr<DayType>> Get(string name, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the day type by its <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The identifier of the day type.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns></returns>
	Task<ErrorOr<DayType>> Get(int id, bool trackChanges = false, CancellationToken cancellationToken = default);
}
