using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Models.Finance;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The card repository interface.
/// </summary>
/// <inheritdoc/>
public interface ICardRepository : IIdentityRepository<CardModel>
{ }
