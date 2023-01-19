using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Finances.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.Contexts.Finances;

/// <summary>
/// The transaction repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAccountRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TransactionRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public TransactionRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
