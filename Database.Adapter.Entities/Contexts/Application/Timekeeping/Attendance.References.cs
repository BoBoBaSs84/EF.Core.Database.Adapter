using Database.Adapter.Entities.Contexts.Application.Authentication;
using Database.Adapter.Entities.Contexts.Application.MasterData;

namespace Database.Adapter.Entities.Contexts.Application.Timekeeping;

public partial class Attendance
{
	/// <summary>The <see cref="User"/> property.</summary>
	public virtual User User { get; set; } = default!;

	/// <summary>The <see cref="CalendarDay"/> property.</summary>
	public virtual CalendarDay CalendarDay { get; set; } = default!;

	/// <summary>The <see cref="DayType"/> property.</summary>
	public virtual DayType DayType { get; set; } = default!;
}
