using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Finances.Interfaces;

/// <summary>
/// The card repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface ICardRepository : IGenericRepository<Card>
{
	/// <summary>
	/// Should get the account entity by the card number.
	/// </summary>
	/// <param name="cardNumber">The number of the card.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A card entity.</returns>
	Card GetCard(string cardNumber, bool trackChanges = false);
	/// <summary>
	/// Should get a collection of card entities by the user identifier.
	/// </summary>
	/// <param name="userId">The identifier of the user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A collection of card entities.</returns>
	IEnumerable<Card> GetCards(int userId, bool trackChanges = false);
}
