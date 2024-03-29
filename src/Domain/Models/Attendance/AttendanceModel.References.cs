﻿using Domain.Models.Common;
using Domain.Models.Identity;

namespace Domain.Models.Attendance;

public partial class AttendanceModel
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; set; } = default!;

	/// <summary>
	/// The <see cref="Calendar"/> property.
	/// </summary>
	public virtual CalendarModel Calendar { get; set; } = default!;
}
