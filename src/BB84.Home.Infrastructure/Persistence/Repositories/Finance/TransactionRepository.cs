using BB84.EntityFrameworkCore.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Finance;
using BB84.Home.Domain.Entities.Finance;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Finance;

/// <summary>
/// Represents the repository for the <see cref="TransactionEntity"/> entity.
/// </summary>
internal sealed class TransactionRepository(IRepositoryContext repositoryContext)
	: IdentityRepository<TransactionEntity>(repositoryContext), ITransactionRepository
{ }
