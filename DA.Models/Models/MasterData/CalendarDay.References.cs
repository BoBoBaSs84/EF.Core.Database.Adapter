using DA.Domain.Models.Timekeeping;

namespace DA.Domain.Models.MasterData;

public partial class CalendarDay
{
	/// <summary>
	/// The <see cref="DayType"/> property.
	/// </summary>
	public virtual DayType DayType { get; set; } = default!;
	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
}
