using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Finance;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The card repository interface.
/// </summary>
/// <inheritdoc/>
public interface ICardRepository : IIdentityRepository<CardEntity>
{ }
