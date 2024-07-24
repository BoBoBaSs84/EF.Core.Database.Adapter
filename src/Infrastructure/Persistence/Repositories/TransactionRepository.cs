using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Finance;

using Infrastructure.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The transaction repository class.
/// </summary>
/// <inheritdoc/>
/// <param name="context">The database context to work with.</param>
internal sealed class TransactionRepository(IRepositoryContext context) : IdentityRepository<TransactionModel>(context), ITransactionRepository
{ }
