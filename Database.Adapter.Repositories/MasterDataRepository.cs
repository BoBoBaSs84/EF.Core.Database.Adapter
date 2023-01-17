using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Application.MasterData;
using Database.Adapter.Repositories.Contexts.Application.MasterData.Interfaces;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

/// <summary>
/// The master data repository manager class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IMasterDataRepository"/> interface</item>
/// </list>
/// </remarks>
public sealed class MasterDataRepository : UnitOfWork<ApplicationContext>, IMasterDataRepository
{
	private readonly Lazy<ICalendarDayRepository> lazyCalendarRepository;
	private readonly Lazy<IDayTypeRepository> lazyDayTypeRepository;

	/// <summary>
	/// Initializes a new instance of the <see cref="MasterDataRepository"/> class.
	/// </summary>
	public MasterDataRepository()
	{
		lazyCalendarRepository = new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(Context));
		lazyDayTypeRepository = new Lazy<IDayTypeRepository>(() => new DayTypeRepository(Context));
	}

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarRepository => lazyCalendarRepository.Value;
	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository => lazyDayTypeRepository.Value;
}
