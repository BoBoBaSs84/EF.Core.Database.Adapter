using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Database.Adapter.Repositories.Contexts.Application.MasterData.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

/// <summary>
/// The master data repository manager interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IUnitOfWork{TContext}"/> interface</item>
/// </list>
/// </remarks>
public interface IMasterDataRepository : IUnitOfWork<ApplicationContext>
{
	/// <summary>The <see cref="CalendarRepository"/> interface.</summary>
	ICalendarDayRepository CalendarRepository { get; }
	/// <summary>The <see cref="DayTypeRepository"/> interface.</summary>
	IDayTypeRepository DayTypeRepository { get; }
}
