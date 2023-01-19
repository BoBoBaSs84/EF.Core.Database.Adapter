using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager : UnitOfWork<ApplicationContext>, IRepositoryManager
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
