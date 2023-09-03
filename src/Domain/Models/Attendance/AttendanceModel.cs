using System.ComponentModel.DataAnnotations.Schema;

using Domain.Enumerators;
using Domain.Models.Base;

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
	/// The <see cref="CalendarDayId"/> property.
	/// </summary>
	public Guid CalendarDayId { get; set; }

	/// <summary>
	/// The day type property.
	/// </summary>
	public DayType DayType { get; set; }

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
