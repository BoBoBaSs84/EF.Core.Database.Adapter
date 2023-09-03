using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models.Base;

namespace Domain.Models.Attendance;

/// <summary>
/// The user setting model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class AttendanceSettingsModel : AuditedModel
{
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The work days per week property.
	/// </summary>
	public int WorkDays { get; set; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	public float WorkHours { get; set; } 
}
