using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Database.Adapter.Repositories.Context.MasterData.Interfaces;

namespace Database.Adapter.Repositories.Context.Interfaces;

/// <summary>
/// The master data repository manager interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IUnitOfWork{TContext}"/>(<see cref="MasterDataContext"/>) interface.</item>
/// </list>
/// </remarks>
public interface IMasterDataRepository : IUnitOfWork<MasterDataContext>
{
	/// <summary>The <see cref="CalendarRepository"/> interface.</summary>
	ICalendarDayRepository CalendarRepository { get; }
	/// <summary>The <see cref="DayTypeRepository"/> interface.</summary>
	IDayTypeRepository DayTypeRepository { get; }
}
