using Database.Adapter.Entities.Contexts.Application.Timekeeping;

namespace Database.Adapter.Entities.Contexts.Application.MasterData;

public partial class DayType
{
	/// <summary>The <see cref="CalendarDays"/> property.</summary>
	public virtual ICollection<CalendarDay> CalendarDays { get; set; } = default!;
	/// <summary>The <see cref="Attendances"/> property.</summary>
	public virtual ICollection<Attendance> Attendances { get; set; } = default!;
}
