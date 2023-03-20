﻿using Domain.Entities.Private;

namespace Domain.Entities.Enumerator;

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