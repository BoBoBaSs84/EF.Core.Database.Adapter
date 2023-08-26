using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Entities.Finance;

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
	/// Initializes a new instance of the <see cref="TransactionRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public TransactionRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
