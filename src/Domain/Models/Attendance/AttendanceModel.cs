﻿using System.ComponentModel.DataAnnotations.Schema;

using BB84.EntityFrameworkCore.Models;

using Domain.Enumerators;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;

namespace Domain.Models.Attendance;

/// <summary>
/// The attendance entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class AttendanceModel : AuditedModel
{
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The <see cref="CalendarId"/> property.
	/// </summary>
	public Guid CalendarId { get; set; }

	/// <summary>
	/// The attendance type property.
	/// </summary>
	public AttendanceType AttendanceType { get; set; }

	/// <summary>
	/// The <see cref="StartTime"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.TIME0)]
	public TimeSpan? StartTime { get; set; } = default!;

	/// <summary>
	/// The <see cref="EndTime"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.TIME0)]
	public TimeSpan? EndTime { get; set; } = default!;

	/// <summary>
	/// The <see cref="BreakTime"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.TIME0)]
	public TimeSpan? BreakTime { get; set; } = default!;
}
