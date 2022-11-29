using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Interfaces;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Data.Interfaces;

/// <summary>
/// The master data repository manager interface.
/// </summary>
/// <remarks>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IUnitOfWork{TContext}"/> interface.</item>
/// </list>
/// </remarks>
public interface IMasterDataRepository : IUnitOfWork<MasterDataContext>
{
	/// <summary>
	/// The <see cref="CalendarRepository"/> interface.
	/// </summary>
	IGenericRepository<Calendar> CalendarRepository { get; }
}
