using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Interfaces;
using Database.Adapter.Repositories.MasterData;
using Database.Adapter.Repositories.MasterData.Interfaces;

namespace Database.Adapter.Repositories;

/// <summary>
/// The master data repository manager class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interface <see cref="IMasterDataRepository"/>.
/// </remarks>
public sealed class MasterDataRepository : UnitOfWork<MasterDataContext>, IMasterDataRepository
{
	private readonly Lazy<ICalendarRepository> lazyCalendarRepository;

	/// <summary>
	/// The master data repository manager standard constructor.
	/// </summary>
	public MasterDataRepository()
	{
		lazyCalendarRepository = new Lazy<ICalendarRepository>(() => new CalendarRepository(dbContext));
	}

	/// <inheritdoc/>
	public ICalendarRepository CalendarRepository => lazyCalendarRepository.Value;
}
