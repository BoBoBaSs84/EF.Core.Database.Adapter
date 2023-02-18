using DA.Repositories.Contexts.MasterData.Interfaces;

namespace DA.Repositories.Manager.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>
	/// The <see cref="CalendarRepository"/> interface.
	/// </summary>
	ICalendarDayRepository CalendarRepository { get; }
	/// <summary>
	/// The <see cref="DayTypeRepository"/> interface.
	/// </summary>
	IDayTypeRepository DayTypeRepository { get; }
	/// <summary>
	/// The <see cref="CardTypeRepository"/> interface.
	/// </summary>
	ICardTypeRepository CardTypeRepository { get; }
}
