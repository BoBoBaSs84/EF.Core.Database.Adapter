using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<ICalendarDayRepository> lazyCalendarRepository;
	private readonly Lazy<IDayTypeRepository> lazyDayTypeRepository;
	private readonly Lazy<ICardTypeRepository> lazyCardTypeRepository;

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarRepository => lazyCalendarRepository.Value;
	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository => lazyDayTypeRepository.Value;
	/// <inheritdoc/>
	public ICardTypeRepository CardTypeRepository => lazyCardTypeRepository.Value;
}
