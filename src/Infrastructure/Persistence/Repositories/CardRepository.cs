using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Finance;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The card repository class.
/// </summary>
/// <param name="context">The database context to work with.</param>
internal sealed class CardRepository(IRepositoryContext context) : IdentityRepository<CardModel>(context), ICardRepository
{ }
