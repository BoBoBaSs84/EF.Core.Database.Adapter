using Domain.Models.Identity;

namespace Domain.Models.Attendance;

public partial class AttendanceModel
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; set; } = default!;
}
