using Application.Contracts.Responses.Enumerator;

using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The card type service interface.
/// </summary>
public interface ICardTypeService
{
	/// <summary>
	/// Should return all card type entities.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A paged list response.</returns>
	Task<ErrorOr<IEnumerable<CardTypeResponse>>> GetAll(bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the card type by its name.
	/// </summary>
	/// <param name="name">The name of the day type.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns></returns>
	Task<ErrorOr<CardTypeResponse>> GetByName(string name, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the card type by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the day type.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns></returns>
	Task<ErrorOr<CardTypeResponse>> GetById(int id, bool trackChanges = false, CancellationToken cancellationToken = default);
}
