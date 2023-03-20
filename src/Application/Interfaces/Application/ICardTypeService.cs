using Application.Contracts.Responses;
using Application.Features.Requests;
using Application.Features.Responses;
using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The card type service interface.
/// </summary>
internal interface ICardTypeService
{
	/// <summary>
	/// Should return the card type entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A paged list response.</returns>
	Task<ErrorOr<IPagedList<CardTypeResponse>>> GetPagedByParameters(CardTypeParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

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
