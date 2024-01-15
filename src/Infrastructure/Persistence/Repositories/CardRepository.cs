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
/// <remarks>
/// Initializes a new instance of the card repository class.
/// </remarks>
/// <inheritdoc/>
internal sealed class CardRepository(DbContext dbContext) : IdentityRepository<CardModel>(dbContext), ICardRepository
{
}
