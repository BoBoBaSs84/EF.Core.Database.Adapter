using Database.Adapter.Repositories.Contexts.MasterData.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>The <see cref="CalendarRepository"/> interface.</summary>
	ICalendarDayRepository CalendarRepository { get; }
	/// <summary>The <see cref="DayTypeRepository"/> interface.</summary>
	IDayTypeRepository DayTypeRepository { get; }
}
