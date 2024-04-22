using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The card repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class CardRepository(DbContext dbContext) : IdentityRepository<CardModel>(dbContext), ICardRepository
{ }
