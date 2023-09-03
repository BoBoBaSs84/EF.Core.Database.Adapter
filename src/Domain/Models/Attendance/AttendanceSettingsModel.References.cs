using Domain.Entities.Identity;

namespace Domain.Models.Attendance;

public partial class AttendanceSettingsModel
{
	/// <summary>
	/// The navigational user property.
	/// </summary>
	public virtual User User { get; set; } = default!;
}
