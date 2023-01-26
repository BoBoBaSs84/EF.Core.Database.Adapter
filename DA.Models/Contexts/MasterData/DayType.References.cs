using DA.Models.Contexts.Timekeeping;

namespace DA.Models.Contexts.MasterData;

public partial class DayType
{
	/// <summary>
	/// The <see cref="CalendarDays"/> property.
	/// </summary>
	public virtual ICollection<CalendarDay> CalendarDays { get; set; } = new HashSet<CalendarDay>();
	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
}
