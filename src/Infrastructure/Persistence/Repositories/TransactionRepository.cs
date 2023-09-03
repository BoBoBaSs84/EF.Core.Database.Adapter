using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Models.Finance;

using Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The transaction repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class
/// and implements the <see cref="ITransactionRepository"/> interface.
/// </remarks>
internal sealed class TransactionRepository : IdentityRepository<Transaction>, ITransactionRepository
{
	/// <summary>
	/// Initializes a new instance of the transaction repository class.
	/// </summary>
	/// <inheritdoc/>
	public TransactionRepository(DbContext dbContext) : base(dbContext)
	{ }
}
