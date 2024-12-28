using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Finance;

namespace Infrastructure.Persistence.Repositories.Finance;

/// <summary>
/// The card repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class CardRepository(IDbContext dbContext) : IdentityRepository<CardEntity>(dbContext), ICardRepository
{ }
