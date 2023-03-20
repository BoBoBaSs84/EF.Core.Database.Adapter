﻿using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
using Domain.Entities.Finance;

namespace Application.Interfaces.Infrastructure.Repositories;

/// <summary>
/// The card repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface ICardRepository : IIdentityRepository<Card>
{
	/// <summary>
	/// Should get a card entity by the primary account number.
	/// </summary>
	/// <param name="pan">The number of the card aka. <b>p</b>rimary <b>a</b>ccount <b>n</b>umber.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A card entity.</returns>
	Task<Card> GetCardAsync(string pan, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of card entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of card entities.</returns>
	Task<IEnumerable<Card>> GetCardsAsync(int userId, bool trackChanges = false, CancellationToken cancellationToken = default);
}