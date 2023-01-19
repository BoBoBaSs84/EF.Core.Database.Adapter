using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.Finances.Interfaces;

/// <summary>
/// The transaction repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface ITransactionRepository : IGenericRepository<Transaction>
{
}
