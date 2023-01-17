using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.Contexts.Application.Timekeeping;

/// <summary>
/// The attendance entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class.
/// </remarks>
[Index(nameof(UserId), nameof(CalendarDayId), IsUnique = true)]
[XmlRoot(ElementName = nameof(Attendance), Namespace = XmlNameSpaces.ATTENDANCE_NAMESPACE)]
public partial class Attendance : AuditedModel
{
	/// <summary> The <see cref="UserId"/> property.</summary>
	[XmlAttribute(AttributeName = nameof(UserId), DataType = XmlDataType.INT, Form = XmlSchemaForm.Qualified)]
	public int UserId { get; set; }

	/// <summary> The <see cref="CalendarDayId"/> property.</summary>
	[XmlAttribute(AttributeName = nameof(CalendarDayId), DataType = XmlDataType.INT, Form = XmlSchemaForm.Qualified)]
	public int CalendarDayId { get; set; }

	/// <summary> The <see cref="DayTypeId"/> property.</summary>
	[XmlAttribute(AttributeName = nameof(DayTypeId), DataType = XmlDataType.INT, Form = XmlSchemaForm.Qualified)]
	public int DayTypeId { get; set; }

	/// <summary> The <see cref="StartTime"/> property.</summary>
	[Column(TypeName = SqlDataType.TIME0)]
	[XmlElement(DataType = XmlDataType.TIME, ElementName = nameof(StartTime), Form = XmlSchemaForm.Qualified)]
	public TimeSpan? StartTime { get; set; }

	/// <summary> The <see cref="EndTime"/> property.</summary>
	[Column(TypeName = SqlDataType.TIME0)]
	[XmlElement(DataType = XmlDataType.TIME, ElementName = nameof(EndTime), Form = XmlSchemaForm.Qualified)]
	public TimeSpan? EndTime { get; set; }

	/// <summary> The <see cref="BreakTime"/> property.</summary>
	[Column(TypeName = SqlDataType.TIME0)]
	[XmlElement(DataType = XmlDataType.TIME, ElementName = nameof(BreakTime), Form = XmlSchemaForm.Qualified)]
	public TimeSpan? BreakTime { get; set; }
}
