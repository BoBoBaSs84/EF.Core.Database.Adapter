using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Interfaces;
using Database.Adapter.Repositories.MasterData.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

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
	ICalendarRepository CalendarRepository { get; }
}
