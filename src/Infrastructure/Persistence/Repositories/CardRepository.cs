using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Models.Finance;

using Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The card repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class
/// and implements the <see cref="ICardRepository"/> interface.
/// </remarks>
internal sealed class CardRepository : IdentityRepository<Card>, ICardRepository
{
	/// <summary>
	/// Initializes a new instance of the card repository class.
	/// </summary>
	/// <inheritdoc/>
	public CardRepository(DbContext dbContext) : base(dbContext)
	{	}
}
