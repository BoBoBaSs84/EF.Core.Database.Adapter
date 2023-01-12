using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Context.Interfaces;
using Database.Adapter.Repositories.Context.MasterData;
using Database.Adapter.Repositories.Context.MasterData.Interfaces;

namespace Database.Adapter.Repositories.Context;

/// <summary>
/// The master data repository manager class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interface <see cref="IMasterDataRepository"/>.
/// </remarks>
public sealed class MasterDataRepository : UnitOfWork<MasterDataContext>, IMasterDataRepository
{
	private readonly Lazy<ICalendarDayRepository> lazyCalendarRepository;
	private readonly Lazy<IDayTypeRepository> lazyDayTypeRepository;

	/// <summary>
	/// The master data repository manager standard constructor.
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
