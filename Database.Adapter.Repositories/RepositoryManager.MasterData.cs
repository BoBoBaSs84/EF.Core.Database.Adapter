using Database.Adapter.Repositories.Contexts.MasterData;
using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private Lazy<ICalendarDayRepository> lazyCalendarRepository = default!;
	private Lazy<IDayTypeRepository> lazyDayTypeRepository = default!;
	private Lazy<ICardTypeRepository> lazyCardTypeRepository = default!;

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarRepository => lazyCalendarRepository.Value;
	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository => lazyDayTypeRepository.Value;
	/// <inheritdoc/>
	public ICardTypeRepository CardTypeRepository => lazyCardTypeRepository.Value;

	private void InitializeMasterData()
	{
		lazyCalendarRepository = new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(DbContext));
		lazyDayTypeRepository = new Lazy<IDayTypeRepository>(() => new DayTypeRepository(DbContext));
		lazyCardTypeRepository = new Lazy<ICardTypeRepository>(() => new CardTypeRepository(DbContext));
	}
}
