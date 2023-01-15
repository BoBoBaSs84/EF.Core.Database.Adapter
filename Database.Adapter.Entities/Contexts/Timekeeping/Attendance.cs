using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.Contexts.Timekeeping;

[Index(nameof(UserId), nameof(CalendarDayId), IsUnique = true)]
[XmlRoot(ElementName = nameof(Attendance), Namespace = XmlNameSpaces.ATTENDANCE_NAMESPACE)]
public sealed class Attendance : AuditedModel
{
	private DateTime? startTime;
	private DateTime? endTime;
	private DateTime? breakTime;

	[XmlAttribute(AttributeName = nameof(UserId), DataType = XmlDataType.INT, Form = XmlSchemaForm.Qualified)]
	public int UserId { get; set; }
	[XmlAttribute(AttributeName = nameof(CalendarDayId), DataType = XmlDataType.INT, Form = XmlSchemaForm.Qualified)]
	public int CalendarDayId { get; set; }
	[XmlAttribute(AttributeName = nameof(DayTypeId), DataType = XmlDataType.INT, Form = XmlSchemaForm.Qualified)]
	public int DayTypeId { get; set; }
	[Column(TypeName = SqlDataType.TIME)]
	[XmlAttribute(AttributeName = nameof(StartTime), DataType = XmlDataType.TIME, Form = XmlSchemaForm.Qualified)]
	public DateTime StartTime { get => startTime.Value; set => startTime = value; }
	[Column(TypeName = SqlDataType.TIME)]
	[XmlAttribute(AttributeName = nameof(EndTime), DataType = XmlDataType.TIME, Form = XmlSchemaForm.Qualified)]
	public DateTime EndTime { get => endTime.Value; set => endTime = value; }
	[Column(TypeName = SqlDataType.TIME)]
	[XmlAttribute(AttributeName = nameof(BreakTime), DataType = XmlDataType.TIME, Form = XmlSchemaForm.Qualified)]
	public DateTime BreakTime { get => breakTime.Value; set => breakTime = value; }

	public bool ShouldSerializeStartTime() => startTime.HasValue;
	public bool ShouldSerializeEndTime() => endTime.HasValue;
	public bool ShouldSerializeBreakTime() => breakTime.HasValue;
}
