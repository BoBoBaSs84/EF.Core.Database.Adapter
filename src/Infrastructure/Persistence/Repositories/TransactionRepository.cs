using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The transaction repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class TransactionRepository(DbContext dbContext) : IdentityRepository<TransactionModel>(dbContext), ITransactionRepository
{ }
