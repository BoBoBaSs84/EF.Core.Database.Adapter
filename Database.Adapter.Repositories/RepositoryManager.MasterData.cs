using Database.Adapter.Repositories.Contexts.MasterData;
using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<ICalendarDayRepository> lazyCalendarRepository = default!;
	private readonly Lazy<IDayTypeRepository> lazyDayTypeRepository = default!;
	private readonly Lazy<ICardTypeRepository> lazyCardTypeRepository = default!;

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarRepository =>
		lazyCalendarRepository.Value ?? new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(DbContext)).Value;
	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository =>
		lazyDayTypeRepository.Value ?? new Lazy<IDayTypeRepository>(() => new DayTypeRepository(DbContext)).Value;
	/// <inheritdoc/>
	public ICardTypeRepository CardTypeRepository
		=> lazyCardTypeRepository.Value ?? new Lazy<ICardTypeRepository>(() => new CardTypeRepository(DbContext)).Value;
}
