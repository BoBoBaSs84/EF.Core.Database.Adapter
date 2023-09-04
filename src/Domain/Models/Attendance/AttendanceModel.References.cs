using Domain.Models.Common;
using Domain.Entities.Identity;

namespace Domain.Models.Attendance;

public partial class AttendanceModel
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; set; } = default!;

	/// <summary>
	/// The <see cref="CalendarDay"/> property.
	/// </summary>
	public virtual CalendarModel CalendarDay { get; set; } = default!;
}
