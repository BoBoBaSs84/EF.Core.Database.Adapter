using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Application.MasterData.Interfaces;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager : UnitOfWork<ApplicationContext>, IRepositoryManager
{
	private readonly Lazy<ICalendarDayRepository> lazyCalendarRepository;
	private readonly Lazy<IDayTypeRepository> lazyDayTypeRepository;

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarRepository => lazyCalendarRepository.Value;
	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository => lazyDayTypeRepository.Value;

}
