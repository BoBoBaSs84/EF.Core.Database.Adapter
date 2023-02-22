﻿using DA.Models.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static DA.Models.Constants.Sql;

namespace DA.Models.Contexts.Timekeeping;

/// <summary>
/// The attendance entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class.
/// </remarks>
[Index(nameof(UserId), nameof(CalendarDayId), IsUnique = true)]
public partial class Attendance : AuditedModel
{
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public int UserId { get; set; } = default!;
	/// <summary>
	/// The <see cref="CalendarDayId"/> property.
	/// </summary>
	public int CalendarDayId { get; set; } = default!;
	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	public int DayTypeId { get; set; } = default!;
	/// <summary>
	/// The <see cref="StartTime"/> property.
	/// </summary>
	[Column(TypeName = DataType.TIME0)]
	public TimeSpan? StartTime { get; set; } = default!;
	/// <summary>
	/// The <see cref="EndTime"/> property.
	/// </summary>
	[Column(TypeName = DataType.TIME0)]
	public TimeSpan? EndTime { get; set; } = default!;
	/// <summary>
	/// The <see cref="BreakTime"/> property.
	/// </summary>
	[Column(TypeName = DataType.TIME0)]
	public TimeSpan? BreakTime { get; set; } = default!;
}