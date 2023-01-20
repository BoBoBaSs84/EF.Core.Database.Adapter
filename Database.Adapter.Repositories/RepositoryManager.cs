using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Authentication;
using Database.Adapter.Repositories.Contexts.Authentication.Interfaces;
using Database.Adapter.Repositories.Contexts.MasterData;
using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;
using Database.Adapter.Repositories.Contexts.Timekeeping;
using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
	/// The <see cref="DbContext"/> property.
	/// </summary>
	public DbContext DbContext { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryManager"/> class.
	/// </summary>
	public RepositoryManager(DbContext? dbContext = null)
	{
		DbContext = (dbContext is null) ? base.Context : dbContext;
		lazyCalendarRepository = new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(DbContext));
		lazyDayTypeRepository = new Lazy<IDayTypeRepository>(() => new DayTypeRepository(DbContext));
		lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(DbContext));
		lazyCardTypeRepository = new Lazy<ICardTypeRepository>(() => new CardTypeRepository(DbContext));
		lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(DbContext));
	}
}
