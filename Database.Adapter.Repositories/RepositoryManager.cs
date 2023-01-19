using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.MasterData;
using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;
using Database.Adapter.Repositories.Contexts.Timekeeping;
using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

/// <summary>
/// The master repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IRepositoryManager"/> interface</item>
/// </list>
/// </remarks>
public sealed partial class RepositoryManager : UnitOfWork<ApplicationContext>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryManager"/> class.
	/// </summary>
	public RepositoryManager()
	{
		lazyCalendarRepository = new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(Context));
		lazyDayTypeRepository = new Lazy<IDayTypeRepository>(() => new DayTypeRepository(Context));
		lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(Context));
		lazyCardTypeRepository = new Lazy<ICardTypeRepository>(() => new CardTypeRepository(Context));
	}
}
