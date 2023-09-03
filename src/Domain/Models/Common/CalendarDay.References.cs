using Domain.Models.Attendance;

namespace Domain.Models.Common;

public partial class CalendarDay
{
	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public virtual ICollection<AttendanceModel> Attendances { get; set; } = new HashSet<AttendanceModel>();
}
